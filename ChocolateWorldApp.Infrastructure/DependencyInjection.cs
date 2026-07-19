using ChocolateWorldApp.Application.Auth.Interfaces;
using ChocolateWorldApp.Application.Common.Interfaces;
using ChocolateWorldApp.Infrastructure.Auth;
using ChocolateWorldApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace ChocolateWorldApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("MyConn")));
        // whoever ask for IAppDbContext(when application tries to look for infra) gets AppDbContext
        //scoped one instance per http request
        services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<AppDbContext>());
        var redisConnectionString = configuration.GetConnectionString("Redis")
        ?? throw new InvalidOperationException("RedisConnectionString not found");
        //Singleton because its expensive -> opens tcp conn, does handshake, negotiate protocol.
        //this is internally thread safe
        services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(redisConnectionString));
        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
        services.AddScoped<IOtpService, RedisOtpService>( );
        services.AddScoped<ISmsSender, StubSmsSender>();
        services.AddScoped<IRefreshTokenStore, RedisRefreshTokenStore>();
        services.AddScoped<ITokenService, JwtTokenService>();
        return services;
    }
    
}