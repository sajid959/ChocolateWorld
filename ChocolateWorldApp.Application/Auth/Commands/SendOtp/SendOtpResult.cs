namespace ChocolateWorldApp.Application.Auth.Commands.SendOtp;

public record SendOtpResult(bool Ok, int ExpiresInSeconds);