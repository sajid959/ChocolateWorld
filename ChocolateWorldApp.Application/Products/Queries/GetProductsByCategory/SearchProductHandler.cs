using ChocolateWorldApp.Application.Common.Interfaces;
using ChocolateWorldApp.Application.Common.Models;
using ChocolateWorldApp.Application.Products.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ChocolateWorldApp.Application.Products.Queries;

public class SearchProductHandler
{
    private readonly IAppDbContext _db;
    private static readonly IReadOnlyList<ProductSummaryDto> EmptyItems  = new List<ProductSummaryDto>(); //or directly we can pass []  
    public SearchProductHandler(IAppDbContext db)
    {
        _db = db;
    }

    public async Task<PagedResult<ProductSummaryDto>> HandleAsync(SearchProductQuery query, CancellationToken cancellationToken)
    {
        var text = query.SearchText.Trim();
        if (text.Length == 0)
        {
            return new PagedResult<ProductSummaryDto>(EmptyItems,0,query.Page,query.PageSize);
        }

        var baseQuery = _db.Products
            .AsNoTracking()
            .Where(p => p.IsActive &&
                        (EF.Functions.Like(p.Name, $"%{text}%") ||
                         EF.Functions.Like(p.OccasionTags, $"%{text}%") ||
                         EF.Functions.Like(p.Description, $"%{text}%")
                        )
            );
        var totalCount = await baseQuery.CountAsync(cancellationToken);
        var items = await baseQuery
            .OrderBy(pd => pd.Name)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(
                pd => new ProductSummaryDto(
                    pd.Id,
                    pd.Name,
                    pd.Slug,
                    pd.OccasionTags,
                    pd.ProductVariants.Min(p => (decimal?)p.Price) ?? 0m
                    )
                ).ToListAsync(cancellationToken);
        
        return new PagedResult<ProductSummaryDto>(items,
            totalCount,
            query.Page,
            query.PageSize);
    }
}