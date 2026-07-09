using ChocolateWorldApp.Application.Categories.Queries.GetAllCategories;
using Microsoft.Extensions.DependencyInjection;

namespace ChocolateWorldApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<GetAllCategoriesHandler>();
        return services;
    }
    
}