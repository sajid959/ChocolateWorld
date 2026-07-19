using ChocolateWorldApp.Domain.Entities;

namespace ChocolateWorldApp.Application.Auth.Interfaces;

public interface ITokenService
{
    string  GenerateAccessToken(User user);
    string GenerateRefreshToken();
    int    AccessTokenExpirySeconds { get; }
}