using ChocolateWorldApp.Application.Auth.Dtos;
using ChocolateWorldApp.Application.Auth.Interfaces;
using ChocolateWorldApp.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChocolateWorldApp.Application.Auth.Commands.RefreshToken;

public class RefreshTokenHandler
{
    private  readonly IAppDbContext _context;
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokenStore _refreshTokenStore;

    public RefreshTokenHandler(IAppDbContext context, ITokenService tokenService, IRefreshTokenStore refreshTokenStore)
    {
        _context = context;
        _tokenService = tokenService;
        _refreshTokenStore = refreshTokenStore;
    }

    public async Task<RefreshTokenResult> HandleAsync(RefreshTokenCommand command, CancellationToken cancellationToken = default)
    {
        if(string.IsNullOrWhiteSpace(command.RefreshToken)) throw new UnauthorizedAccessException("Invalid refresh token");
        
        var userId =await  _refreshTokenStore.ConsumeAndDeleteAsync(command.RefreshToken, cancellationToken)
            ?? throw new UnauthorizedAccessException("Invalid refresh token");
        
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId, cancellationToken)
            ?? throw new UnauthorizedAccessException("Invalid refresh token");
        var accessToken = _tokenService.GenerateAccessToken(user);
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        await _refreshTokenStore.StoreAsync(newRefreshToken, userId, cancellationToken);

        return new RefreshTokenResult(
            AccessToken: accessToken,
            RefreshToken: newRefreshToken,
            ExpiresIn: _tokenService.AccessTokenExpirySeconds,
            User: new AuthUserDto(user.Id,user.Phone,user.Email, user.Role)
            );
    }
    
}