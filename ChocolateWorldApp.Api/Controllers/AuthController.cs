using ChocolateWorldApp.Application.Auth.Commands.RefreshToken;
using ChocolateWorldApp.Application.Auth.Commands.SendOtp;
using ChocolateWorldApp.Application.Auth.Commands.VerifyOtp;
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
    private readonly VerifyOtpHandler _verifyOtpHandler;
    private readonly RefreshTokenHandler _refreshTokenHandler;

    public AuthController(
        SendOtpHandler sendOtpHandler,
        VerifyOtpHandler verifyOtpHandler,
        RefreshTokenHandler refreshTokenHandler)
    {
        _sendOtpHandler = sendOtpHandler;
        _verifyOtpHandler = verifyOtpHandler;
        _refreshTokenHandler = refreshTokenHandler;
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

    /// <summary>
    /// Verify otp entered by user
    /// </summary>
    /// <param name="Phone"></param>
    [HttpPost("otp/verify")]
    public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequest request, CancellationToken cancellationToken)
    {
        var result = await  _verifyOtpHandler.HandleAsync(new VerifyOtpCommand(request.Phone, request.Otp), cancellationToken);
        return Ok(result);
    }

    [HttpPost("refresh")]
    [ProducesResponseType(typeof(RefreshTokenResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
      var result =  await  _refreshTokenHandler.HandleAsync(new RefreshTokenCommand( request.RefreshToken), cancellationToken);
      return Ok(result);
    }
    public record SendOtpRequest(string Phone);
    public record VerifyOtpRequest(string Phone, string Otp);

    public record RefreshTokenRequest(string RefreshToken);
}