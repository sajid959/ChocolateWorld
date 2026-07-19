using ChocolateWorldApp.Application.Auth.Dtos;

namespace ChocolateWorldApp.Application.Auth.Commands.RefreshToken;

public record RefreshTokenResult(
    string AccessToken,
    string RefreshToken,
    int ExpiresIn,
    AuthUserDto User
    );