using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChocolateWorldApp.Api.Controllers;
[ApiController]
[Route("api/me")]
[Authorize]
[Produces("application/json")]
public class MeController : ControllerBase
{
    [HttpGet]
    public IActionResult GetClaims()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                     ?? User.FindFirst("sub")?.Value;
        var phone = User.FindFirst("phone")?.Value;
        var role = User.FindFirst(ClaimTypes.Role)?.Value;
        return Ok(new { userId, phone, role });
    }
}