using ChocolateWorldApp.Application.Products.DTOs;
using ChocolateWorldApp.Application.Products.Queries;
using ChocolateWorldApp.Application.Products.Queries.GetProductDetailsBySlug;
using Microsoft.AspNetCore.Mvc;

namespace ChocolateWorldApp.Api.Controllers;

[ApiController]
[Route( "api/products" )]
[Produces( "application/json" )]
public class ProductsController : ControllerBase
{
    private readonly GetProductDetailsBySlugHandler _getProductDetailsBySlugHandler;
    private readonly SearchProductHandler _searchProductHandler;

    public ProductsController(
        GetProductDetailsBySlugHandler getProductDetailsBySlugHandler,
        SearchProductHandler searchProductHandler)
    {
        _getProductDetailsBySlugHandler = getProductDetailsBySlugHandler;
        _searchProductHandler = searchProductHandler;
    }

    [HttpGet("{slug}")]
    [ProducesResponseType(typeof(ProductDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductDetailsBySlug(string slug, CancellationToken cancellationToken)
    {
        var result =
            await _getProductDetailsBySlugHandler.HandleAsync(new GetProductDetailsBySlugQuery(slug),
                cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductSummaryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SearchProduct([FromQuery] string? search =null,[FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken cancellationToken = default)
    {
        var safeSearch = search ?? string.Empty;
        if(safeSearch.Length > 200) safeSearch = safeSearch[..200];
        var result = await _searchProductHandler.HandleAsync(
            new SearchProductQuery(safeSearch, Math.Max(1,page),
                Math.Clamp(pageSize,1,100)), cancellationToken);
        
        return Ok(result);
    }
}