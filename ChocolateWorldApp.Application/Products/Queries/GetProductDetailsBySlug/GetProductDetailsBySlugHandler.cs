using System.Data;
using System.Text.Json;
using ChocolateWorldApp.Application.Common.Interfaces;
using ChocolateWorldApp.Application.Products.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ChocolateWorldApp.Application.Products.Queries.GetProductDetailsBySlug;

public class GetProductDetailsBySlugHandler
{
    private readonly IAppDbContext _db;

    private static readonly JsonSerializerOptions Jsonopts = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
    
    public GetProductDetailsBySlugHandler(IAppDbContext db)
    {
        _db = db;
    }

    public async Task<ProductDetailsDto> HandleAsync(GetProductDetailsBySlugQuery query,
        CancellationToken cancellationToken)
    {
        var productDetails = await _db.Products
            .AsNoTracking()
            .Where(p => p.Slug == query.Slug && p.IsActive)
            .Select(p => new { p.Id,
                    p.Slug,
                    p.Name,
                    p.Description,
                    p.OccasionTags,
                    p.AttributesJson,
                    Category = _db.Categories
                        .Where(c => c.Id == p.CategoryId)
                        .Select(c => new ProductCategoryRefDto(
                            c.Id,
                            c.Name,
                            c.Slug
                            )).Single(),
                    Variants = p.ProductVariants
                        .OrderBy( v => v.Price)
                        .Select( v => new VariantDto(
                            v.Id,
                            v.BoxSize,
                            v.Price
                            )).ToList()
                }
            ).SingleOrDefaultAsync(cancellationToken) ?? throw new KeyNotFoundException($"Product slug '{query.Slug}' not found  ");
        var attributes = string.IsNullOrWhiteSpace(productDetails.AttributesJson)
            ? new ProductAttributesDto(null, null, null, null)
            : JsonSerializer.Deserialize<ProductAttributesDto>(productDetails.AttributesJson, Jsonopts)
            ?? new ProductAttributesDto(null,null,null,null);
        return new ProductDetailsDto(
            productDetails.Id,
            productDetails.Slug,
            productDetails.Name,
            productDetails.Description,
            productDetails.OccasionTags,
            productDetails.Category,
            attributes,
            productDetails.Variants

        );
    }

}