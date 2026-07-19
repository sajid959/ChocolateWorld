using ChocolateWorldApp.Domain.Entities;

namespace ChocolateWorldApp.Application.Auth.Interfaces;

public interface IRefreshTokenStore
{
    Task StoreAsync (string refreshToken, Guid userId, CancellationToken cancellationToken);
    Task<Guid?> ConsumeAndDeleteAsync(string refreshToken, CancellationToken cancellationToken);
    Task RevokeAllForUserAsync(Guid userId, CancellationToken cancellationToken);
}