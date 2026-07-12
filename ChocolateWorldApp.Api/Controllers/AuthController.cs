using ChocolateWorldApp.Application.Auth.Commands.SendOtp;
using Microsoft.AspNetCore.Mvc;

namespace ChocolateWorldApp.Api.Controllers;

/// <summary>
/// Authenticate - OTP send, verify, token generate and verify.
/// </summary>
[ApiController]
[Route("api/auth")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly SendOtpHandler _sendOtpHandler;

    public AuthController(SendOtpHandler sendOtpHandler)
    {
        _sendOtpHandler = sendOtpHandler;
    }
    /// <summary>
    /// Send a 6 digit OTP for the given number
    /// </summary>
    [HttpPost("otp/send")]
    [ProducesResponseType(typeof(SendOtpResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SendOtp([FromBody] SendOtpRequest request, CancellationToken cancellationToken)
    {
        var result = await _sendOtpHandler.HandleAsync(new SendOtpCommand(request.Phone), cancellationToken);
        return Ok(result);
    }

    public record SendOtpRequest(string Phone);
}