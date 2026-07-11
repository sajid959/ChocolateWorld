using ChocolateWorldApp.Application.Categories.Queries.GetAllCategories;
using ChocolateWorldApp.Application.Categories.Queries.GetCategoryBySlug;
using ChocolateWorldApp.Application.Products.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace ChocolateWorldApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<GetAllCategoriesHandler>();
        services.AddScoped<GetCategoryBySlugHandler>();
        services.AddScoped<GetProductsByCategoryHandler>();
        return services;
    }
    
}