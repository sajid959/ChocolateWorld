using ChocolateWorldApp.Application.Auth.Interfaces;
using Microsoft.Extensions.Logging;

namespace ChocolateWorldApp.Infrastructure.Auth;

public class StubSmsSender : ISmsSender
{
    private readonly ILogger<StubSmsSender> _logger;
    public  StubSmsSender(ILogger<StubSmsSender> logger)
    {
        _logger = logger;
    }

    public Task SendOtpAsync(string phone, string otp, CancellationToken ct)
    {
        _logger.LogInformation($"[DEV SMS] OTP for {phone} is {otp}");
        return Task.CompletedTask;
    }
    
}