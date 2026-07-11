using ChocolateWorldApp.Application.Categories.DTOs;
using ChocolateWorldApp.Application.Common.Interfaces;
using ChocolateWorldApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChocolateWorldApp.Application.Categories.Queries.GetAllCategories;

public class GetAllCategoriesHandler
{
    private readonly IAppDbContext _context;
    public GetAllCategoriesHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<CategoryDto>> HandleAsync(GetAllCategoriesQuery query, CancellationToken cancellationToken)
    {
        return await _context.Categories
            .AsNoTracking()
            .Where(c => c.IsActive)
            .OrderBy(c => c.DisplayOrder)
            .Select(c => new CategoryDto(
                c.Id,
                c.Name,
                c.Slug,
                c.Description,
                c.DisplayOrder)).ToListAsync(cancellationToken);
    }
    
}