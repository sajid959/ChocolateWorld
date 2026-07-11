using System.IO.Pipes;

namespace ChocolateWorldApp.Application.Products.DTOs;

public record ProductAttributesDto(
    IReadOnlyList<string>? Images,
    IReadOnlyList<string>? Ingredients,
    string? AllergenInfo,
    PersonalizationDto? PersonalizationOptions
    );