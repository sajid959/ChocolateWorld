using ChocolateWorldApp.Application.Auth.Commands.RefreshToken;
using ChocolateWorldApp.Application.Auth.Commands.SendOtp;
using ChocolateWorldApp.Application.Auth.Commands.VerifyOtp;
using ChocolateWorldApp.Application.Categories.Queries.GetAllCategories;
using ChocolateWorldApp.Application.Categories.Queries.GetCategoryBySlug;
using ChocolateWorldApp.Application.Products.Queries;
using ChocolateWorldApp.Application.Products.Queries.GetProductDetailsBySlug;
using Microsoft.Extensions.DependencyInjection;

namespace ChocolateWorldApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<GetAllCategoriesHandler>();
        services.AddScoped<GetCategoryBySlugHandler>();
        services.AddScoped<GetProductsByCategoryHandler>();
        services.AddScoped<GetProductDetailsBySlugHandler>();
        services.AddScoped<SearchProductHandler>();
        services.AddScoped<SendOtpHandler>();
        services.AddScoped<VerifyOtpHandler>();
        services.AddScoped<RefreshTokenHandler>();
        return services;
    }
    
}