namespace ChocolateWorldApp.Application.Auth.Interfaces;

public interface IOtpService
{
    Task<string> GenerateAndStoreOtpAsync(string phone, CancellationToken ct);
    Task<bool> ValidateAndConsumeAsync(string phone, string otp , CancellationToken ct);
}