namespace ChocolateWorldApp.Application.Users.Commands;

public record UpdateCommand(Guid UserId, string Name, string? Email);