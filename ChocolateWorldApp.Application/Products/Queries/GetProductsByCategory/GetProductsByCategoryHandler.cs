using ChocolateWorldApp.Application.Common.Interfaces;
using ChocolateWorldApp.Application.Common.Models;
using ChocolateWorldApp.Application.Products.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ChocolateWorldApp.Application.Products.Queries;

public class GetProductsByCategoryHandler
{
    private readonly IAppDbContext _db;
    public GetProductsByCategoryHandler(IAppDbContext db)
    {
        _db = db;
    }

    public async Task<PagedResult<ProductSummaryDto>> HandleAsync(GetProductsByCategoryQuery query,
        CancellationToken cancellationToken)
    {
        var categoryExist = await _db.Categories
            .AsNoTracking()
            .AnyAsync(c => c.Slug == query.Slug && c.IsActive, cancellationToken);
        if (!categoryExist)
        {
            throw new KeyNotFoundException($"Category with Slug {query.Slug} does not exist");
        }

        var baseQuery = _db.Products
            .AsNoTracking()
            .Where(p => p.IsActive && p.Category!.Slug == query.Slug);  //Navigation Property for Join
        var totalCount = await baseQuery.CountAsync(cancellationToken);
        var items = await baseQuery
            .OrderBy(p => p.Name)
            .Skip((query.CurrentPage - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(p => new ProductSummaryDto(
                p.Id,
                p.Name,
                p.Slug,
                p.OccasionTags, 
                p.ProductVariants
                    .Min(pv => (decimal?)pv.Price) ?? 0m)).ToListAsync(cancellationToken);
        
        return new PagedResult<ProductSummaryDto>(items, query.CurrentPage, query.PageSize, totalCount);

    }
}