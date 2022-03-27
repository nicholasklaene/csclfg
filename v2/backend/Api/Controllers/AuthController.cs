using Api.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] AuthSignupCommand request)
    {
        var response = await _mediator.Send(request);
        return response.Errors.Count > 0 ? Conflict(response) : StatusCode(201);
    }
    
    [HttpPost("signin")]
    public async Task<IActionResult> Signin([FromBody] AuthSigninCommand request)
    {
        var response = await _mediator.Send(request);
        if (response.Errors.Count > 0) return BadRequest(response);
        Response.Cookies.Append("refresh_token", response.RefreshToken, new CookieOptions { 
            Secure = true, HttpOnly = true, SameSite = SameSiteMode.Strict, Path = "/auth/refresh"
        });
        return Ok(response);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["refresh_token"];
        if (refreshToken is null) return BadRequest();
        var request = new AuthRefreshCommand { RefreshToken = refreshToken };
        var response = await _mediator.Send(request);
        return response.Errors.Count > 0 ? BadRequest() : Ok(response);
    }
}
