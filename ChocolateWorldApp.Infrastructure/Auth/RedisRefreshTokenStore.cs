using ChocolateWorldApp.Application.Auth.Interfaces;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace ChocolateWorldApp.Infrastructure.Auth;

public class RedisRefreshTokenStore : IRefreshTokenStore
{
    private readonly IDatabase _redis;
    private readonly TimeSpan _ttl;

    public RedisRefreshTokenStore(IConnectionMultiplexer redis,  IOptions<JwtOptions> options)
    {
        _redis = redis.GetDatabase();
        _ttl = TimeSpan.FromDays(options.Value.RefreshTokenDays);
    }

    public Task StoreAsync(string refreshToken, Guid userId, CancellationToken cancellationToken) =>
        _redis.StringSetAsync(Key(refreshToken), userId.ToString(), _ttl);

    public async Task<Guid?> ConsumeAndDeleteAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var key = Key(refreshToken);
        var value = await _redis.StringGetDeleteAsync(key);
        return value.IsNullOrEmpty ? null : Guid.Parse(value.ToString());
    }

    public Task RevokeAllForUserAsync(Guid userId, CancellationToken cancellationToken) => Task.CompletedTask;
    
    private static string Key(string refreshToken) => $"refresh:{refreshToken}";
}