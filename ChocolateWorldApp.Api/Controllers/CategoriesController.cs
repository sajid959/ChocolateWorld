using ChocolateWorldApp.Application.Categories.DTOs;
using ChocolateWorldApp.Application.Categories.Queries.GetAllCategories;
using ChocolateWorldApp.Application.Categories.Queries.GetCategoryBySlug;
using ChocolateWorldApp.Application.Products.DTOs;
using ChocolateWorldApp.Application.Products.Queries;
using ChocolateWorldApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChocolateWorldApp.Api.Controllers;

[ApiController]
[Route("api/categories")]
[Produces("application/json")]
public class CategoriesController : ControllerBase
{
    private readonly GetAllCategoriesHandler _getAllCategoriesHandler;
    private readonly GetCategoryBySlugHandler _getCategoryBySlugHandler;
    private readonly GetProductsByCategoryHandler _getProductsByCategoryHandler;
    

    public CategoriesController(
        GetAllCategoriesHandler getAllCategoriesHandler,
        GetCategoryBySlugHandler getCategoryBySlugHandler,
        GetProductsByCategoryHandler getProductsByCategoryHandler)
    {
        _getAllCategoriesHandler = getAllCategoriesHandler;
        _getCategoryBySlugHandler = getCategoryBySlugHandler;
        _getProductsByCategoryHandler = getProductsByCategoryHandler;
    }

    [HttpGet("/")]
    [ProducesResponseType(typeof(IReadOnlyList<CategoryDto>), 200)]
    public async Task<IActionResult> GetAllCategories(CancellationToken cancellationToken)
    {
        var result = await _getAllCategoriesHandler.HandleAsync(new GetAllCategoriesQuery(),cancellationToken);
        return Ok(result);
    }

    [HttpGet("{slug}")]
    [ProducesResponseType(typeof(CategoryDto), 200)]
    public async Task<IActionResult> GetCategoryBySlug(string slug, CancellationToken cancellationToken)
    {
        var result = await _getCategoryBySlugHandler.HandleAsync(new GetCategoryBySlugQuery(slug), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{slug}/products")]
    [ProducesResponseType(typeof(ProductSummaryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductsByCategory(string slug,[FromQuery] int page = 1, [FromQuery] int pageSize =20, CancellationToken cancellationToken = default)
    {
        var result = await _getProductsByCategoryHandler.HandleAsync(
            new GetProductsByCategoryQuery(slug, Math.Max(1,page),
                Math.Clamp(pageSize,1,100)), cancellationToken);
        return Ok(result);
    }
    
}