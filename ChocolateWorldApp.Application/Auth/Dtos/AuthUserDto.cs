namespace ChocolateWorldApp.Application.Auth.Dtos;

public record AuthUserDto(
    Guid Id,
    string Phone,
    string? Name,
    string Role
    );