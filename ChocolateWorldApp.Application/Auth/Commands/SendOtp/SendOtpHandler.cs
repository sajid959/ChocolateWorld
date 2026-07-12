using ChocolateWorldApp.Application.Auth.Interfaces;

namespace ChocolateWorldApp.Application.Auth.Commands.SendOtp;

public class SendOtpHandler
{
    public const int OtpTtlSeconds = 300;
    private readonly IOtpService _otpService;
    private readonly ISmsSender _smsSender;

    public SendOtpHandler(IOtpService otpService, ISmsSender smsSender)
    {
        _otpService = otpService;
        _smsSender = smsSender;
    }

    public async Task<SendOtpResult> HandleAsync(SendOtpCommand command, CancellationToken cancellationToken)
    {
        var phone = NormalizePhone(command.Phone);
        var otp = await _otpService.GenerateAndStoreOtpAsync(phone, cancellationToken);
        await _smsSender.SendOtpAsync(phone, otp, cancellationToken);
        
        return new SendOtpResult(Ok: true, ExpiresInSeconds: OtpTtlSeconds);
    }

    private static string NormalizePhone(string phone)
    {
        var trimmed = new string(phone.Trim().Where(char.IsDigit).ToArray());
        if(trimmed.Length == 10) return "+91"+trimmed;
        if(trimmed.Length == 12 && trimmed.StartsWith("91")) return "+" + trimmed;
        throw new ArgumentException("Phone Invalid must contain 10 characters", nameof(phone));
    }
}