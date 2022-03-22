using api.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("/users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpDelete("/{username}")]
    [Authorize(Roles = "user")]
    public async Task<IActionResult> DeleteUser([FromRoute] string username)
    {
        var command = new DeleteUserCommand(username);
        var result = await _mediator.Send(command);
        return result ? NoContent() : NotFound();
    }
}
