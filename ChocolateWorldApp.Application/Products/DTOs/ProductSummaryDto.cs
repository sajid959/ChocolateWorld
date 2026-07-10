namespace ChocolateWorldApp.Application.Products.DTOs;

public record ProductSummaryDto(
    Guid Id,
    string Name,
    string Slug,
    string OccasionTags,
    decimal PriceFrom
    );