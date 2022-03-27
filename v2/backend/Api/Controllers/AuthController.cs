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
}
