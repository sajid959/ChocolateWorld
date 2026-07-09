using ChocolateWorldApp.Application.Categories.DTOs;
using ChocolateWorldApp.Application.Categories.Queries.GetAllCategories;
using ChocolateWorldApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChocolateWorldApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CategoriesController : ControllerBase
{
    private readonly GetAllCategoriesHandler _handler;

    public CategoriesController(GetAllCategoriesHandler handler)
    {
        _handler = handler;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<CategoryDto>), 200)]
    public async Task<IActionResult> GetAllCategories(CancellationToken cancellationToken)
    {
        var result = await _handler.HandleAsync(new GetAllCategoriesQuery(),cancellationToken);
        return Ok(result);
    }
    
}