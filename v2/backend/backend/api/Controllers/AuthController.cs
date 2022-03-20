using Microsoft.AspNetCore.Mvc;
using api.Queries;
using MediatR;

namespace api.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] AuthSigninQuery query)
    {
        var response = await _mediator.Send(query);

        if (response.RefreshToken != null)
        {
            var cookieOptions = new CookieOptions() { Secure = true, HttpOnly = true, SameSite = SameSiteMode.None };
            Response.Cookies.Append("refresh_token", response.RefreshToken, cookieOptions);
            response.RefreshToken = null;
        }
        
        return response.Errors.Count > 0 ? BadRequest(response) : Ok(response);
    }
}
