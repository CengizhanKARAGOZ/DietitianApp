using DietApp.Application.Features.Auth.Commands.ChangePassword;
using DietApp.Application.Features.Auth.Commands.ForgotPassword;
using DietApp.Application.Features.Auth.Commands.Login;
using DietApp.Application.Features.Auth.Commands.RefreshToken;
using DietApp.Application.Features.Auth.Commands.Register;
using DietApp.Application.Features.Auth.Commands.ResetPassword;
using DietApp.Application.Features.Auth.DTOs;
using DietApp.Application.Features.Auth.Queries.GetCurrentUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DietApp.API.Controllers;

public class AuthController : BaseController
{
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var command = new LoginCommand(
            request.Email,
            request.Password,
            GetIpAddress(),
            GetUserAgent()
        );

        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var command = new RefreshTokenCommand(
            request.RefreshToken,
            GetIpAddress(),
            GetUserAgent()
        );

        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("forgot-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
    {
        await Mediator.Send(command);
        return Ok(new { message = "Şifre sıfırlama bağlantısı e-posta adresinize gönderildi." });
    }

    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
    {
        await Mediator.Send(command);
        return Ok(new { message = "Şifreniz başarıyla sıfırlandı." });
    }

    [Authorize]
    [HttpPost("change-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
    {
        await Mediator.Send(command);
        return Ok(new { message = "Şifreniz başarıyla değiştirildi." });
    }

    [Authorize]
    [HttpGet("me")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetCurrentUser()
    {
        var result = await Mediator.Send(new GetCurrentUserQuery());
        return Ok(result);
    }

    private string? GetIpAddress()
    {
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
        {
            return Request.Headers["X-Forwarded-For"].FirstOrDefault();
        }

        return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
    }

    private string? GetUserAgent()
    {
        return Request.Headers.UserAgent.FirstOrDefault();
    }
}

public record LoginRequest(string Email, string Password);
public record RefreshTokenRequest(string RefreshToken);
