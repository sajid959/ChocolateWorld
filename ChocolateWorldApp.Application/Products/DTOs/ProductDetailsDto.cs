using ChocolateWorldApp.Domain.Entities;

namespace ChocolateWorldApp.Application.Products.DTOs;

public record ProductDetailsDto(
    Guid Id,
    string Slug,
    string Name,
    string Description,
    string OccasionTags,
    ProductCategoryRefDto Category,
    ProductAttributesDto Attributes,
    IReadOnlyList<VariantDto>  Variants
    );