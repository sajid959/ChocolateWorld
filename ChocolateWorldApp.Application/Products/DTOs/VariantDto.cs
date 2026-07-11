namespace ChocolateWorldApp.Application.Products.DTOs;

public record VariantDto(
    Guid Id,
    string? BoxSize,
    decimal Price
    );