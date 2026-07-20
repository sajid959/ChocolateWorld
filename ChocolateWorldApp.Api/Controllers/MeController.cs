using System.Security.Claims;
using ChocolateWorldApp.Api.Common;
using ChocolateWorldApp.Application.Users.Commands;
using ChocolateWorldApp.Application.Users.Dtos;
using ChocolateWorldApp.Application.Users.Queries;
using ChocolateWorldApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChocolateWorldApp.Api.Controllers;
[ApiController]
[Route("api/me")]
[Authorize]
[Produces("application/json")]
public class MeController : ControllerBase
{
    private readonly GetMeHandler _getMeHandler;
    private readonly UpdateMeHandler _updateMeHandler;

    public MeController( GetMeHandler getMeHandler, UpdateMeHandler updateMeHandler)
    {
        _getMeHandler=  getMeHandler;
        _updateMeHandler = updateMeHandler;
    }
    [HttpGet("getClaim")]
    public IActionResult GetClaims()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                     ?? User.FindFirst("sub")?.Value;
        var phone = User.FindFirst("phone")?.Value;
        var role = User.FindFirst(ClaimTypes.Role)?.Value;
        return Ok(new { userId, phone, role });
    }

    [HttpGet]
    [ProducesResponseType(typeof(MeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMe(CancellationToken cancellationToken)
    {
        return Ok(await _getMeHandler.HandleAsync(new GetMeQuery(CurrentUser.Id(User)) ,cancellationToken));
    }

    [HttpPut]
    [ProducesResponseType(typeof(MeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateMe([FromBody] UpdateMeRequest request, CancellationToken cancellationToken)
    {
        return Ok(await _updateMeHandler.HandleAsync(new UpdateCommand(CurrentUser.Id(User), request.Name, request.Email), cancellationToken));
    }

    public record UpdateMeRequest(string Name , string? Email);
}