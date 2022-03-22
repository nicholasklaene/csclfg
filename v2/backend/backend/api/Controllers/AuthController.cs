using api.Commands;
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
        query.Response = Response;
        var response = await _mediator.Send(query);
        return response.Errors.Count > 0 ? BadRequest(response) : Ok(response);
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] AuthSignupCommand command)
    {
        var response = await _mediator.Send(command);
        return response.Errors.Count > 0 ? BadRequest(response) : StatusCode(201, response);
    }
}
