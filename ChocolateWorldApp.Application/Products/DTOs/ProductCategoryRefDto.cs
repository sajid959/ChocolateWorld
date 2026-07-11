namespace ChocolateWorldApp.Application.Products.DTOs;

public record ProductCategoryRefDto(
    Guid Id,
    string Name,
    string Slug
    );