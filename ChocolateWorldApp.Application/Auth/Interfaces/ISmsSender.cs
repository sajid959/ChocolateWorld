namespace ChocolateWorldApp.Application.Auth.Interfaces;

public interface ISmsSender
{
    Task SendOtpAsync(string phone,string otp, CancellationToken ct);
}