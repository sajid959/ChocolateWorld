namespace ChocolateWorldApp.Application.Users.Dtos;

public record MeDto(
    Guid Id,
    string Name,
    string? Email,
    string Phone,
    string Role,
    DateTimeOffset CreatedAt
    );