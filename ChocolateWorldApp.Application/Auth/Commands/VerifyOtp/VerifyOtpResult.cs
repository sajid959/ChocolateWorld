using ChocolateWorldApp.Application.Auth.Dtos;

namespace ChocolateWorldApp.Application.Auth.Commands.VerifyOtp;

public record VerifyOtpResult(
    string AccessToken,
    string RefreshToken,
    int ExpiresIn,
    AuthUserDto User
    );