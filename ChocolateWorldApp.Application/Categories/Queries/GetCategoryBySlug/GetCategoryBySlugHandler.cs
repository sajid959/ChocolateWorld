using ChocolateWorldApp.Application.Categories.DTOs;
using ChocolateWorldApp.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChocolateWorldApp.Application.Categories.Queries.GetCategoryBySlug;

public class GetCategoryBySlugHandler
{
    private readonly IAppDbContext _dbContext;

    public GetCategoryBySlugHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CategoryDto> HandleAsync(GetCategoryBySlugQuery query, CancellationToken cancellationToken)
    {
        var category = await _dbContext.Categories
            .AsNoTracking()
            .Where(c => c.IsActive && c.Slug == query.Slug)
            .Select(c => new CategoryDto(
                c.Id,
                c.Name,
                c.Slug,
                c.Description,
                c.DisplayOrder
            )).SingleOrDefaultAsync(cancellationToken);
        
        return category ?? throw new KeyNotFoundException($"Category with Slug {query.Slug} does not exist");

    }
}