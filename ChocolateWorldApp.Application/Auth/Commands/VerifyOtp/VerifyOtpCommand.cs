namespace ChocolateWorldApp.Application.Auth.Commands.VerifyOtp;

public record VerifyOtpCommand(string Phone, string Otp);