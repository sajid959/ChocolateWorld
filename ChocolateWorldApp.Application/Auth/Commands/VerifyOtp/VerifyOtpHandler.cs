using ChocolateWorldApp.Application.Auth.Dtos;
using ChocolateWorldApp.Application.Auth.Interfaces;
using ChocolateWorldApp.Application.Common.Interfaces;
using ChocolateWorldApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChocolateWorldApp.Application.Auth.Commands.VerifyOtp;

public class VerifyOtpHandler
{
    private const string DefaultRole = "Customer";
    private readonly IAppDbContext _context;
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokenStore _refreshTokenStore;
    private readonly IOtpService _otpService;

    public VerifyOtpHandler(IAppDbContext context, IRefreshTokenStore refreshTokenStore, ITokenService tokenService, IOtpService otpService)
    {
        
        _context = context;
        _refreshTokenStore = refreshTokenStore;
        _tokenService = tokenService;
        _otpService = otpService;
    }

    public async Task<VerifyOtpResult> HandleAsync(VerifyOtpCommand command, CancellationToken cancellationToken = default)
    {
        var phone = NormalizePhone(command.Phone);
        var isValidOtp = await _otpService.ValidateAndConsumeAsync(phone, command.Otp, cancellationToken);
        if (!isValidOtp) throw new UnauthorizedAccessException("Invalid or Expired OTP");
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Phone == phone, cancellationToken);
        if (user is null)
        {
            user = new User(
                id: Guid.NewGuid(),
                name: "",
                email: null,
                phone: phone,
                role: DefaultRole,
                cretedAt: DateTimeOffset.UtcNow
                );
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
        }
        
        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();
        await _refreshTokenStore.StoreAsync(refreshToken,user.Id, cancellationToken);
        
        return new VerifyOtpResult(
            AccessToken: accessToken,
            RefreshToken: refreshToken,
            ExpiresIn: _tokenService.AccessTokenExpirySeconds,
            User: new AuthUserDto(user.Id, user.Phone,user.Name, user.Role) );
    }
    
    private static string NormalizePhone(string phone)
    {
        var trimmed = new string(phone.Trim().Where(char.IsDigit).ToArray());
        if(trimmed.Length == 10) return "+91"+trimmed;
        if(trimmed.Length == 12 && trimmed.StartsWith("91")) return "+" + trimmed;
        throw new ArgumentException("Phone Invalid must contain 10 characters", nameof(phone));
    }
}