using System.ComponentModel.DataAnnotations;
using ChocolateWorldApp.Application.Auth.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StackExchange.Redis;
using IDatabase = StackExchange.Redis.IDatabase;

namespace ChocolateWorldApp.Infrastructure.Auth;

public class RedisOtpService : IOtpService
{
    private static readonly TimeSpan Ttl = TimeSpan.FromMinutes(5);
    private readonly IDatabase _redisDb;

    public RedisOtpService(IConnectionMultiplexer redisDb)
    {
        _redisDb = redisDb.GetDatabase();
    }

    public async Task<string> GenerateAndStoreOtpAsync(string phone, CancellationToken cancellationToken)
    {
        var otp = Random.Shared.Next(100_000,1_000_000).ToString();  //100_1000 equal 100000 just for readability.
        await _redisDb.StringSetAsync(Key(phone), otp, Ttl);
        return otp;
    }

    public async Task<bool> ValidateAndConsumeAsync(string phone, string otp, CancellationToken cancellationToken)
    {
     var key = Key(phone);
     var stored = await _redisDb.StringGetAsync(key);
     if (stored.IsNullOrEmpty || stored != otp) return false;
     await _redisDb.KeyDeleteAsync(key);
     return true;
    }
    private string Key(string phone) => $"otp:{phone}";
}