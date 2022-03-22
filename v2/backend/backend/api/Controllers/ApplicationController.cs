using api.Commands;
using api.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("/applications")]
public class ApplicationController : ControllerBase
{
    private readonly IMediator _mediator;

    public ApplicationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllApplications()
    {
        var query = new GetAllApplicationsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{applicationId}")]
    public async Task<IActionResult> GetApplicationById([FromRoute] short applicationId)
    {
        var query = new GetApplicationByIdQuery(applicationId);
        var result = await _mediator.Send(query);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> CreateApplication([FromBody] CreateApplicationCommand command)
    {
        var response = await _mediator.Send(command);
        return response == null ? StatusCode(500) : Created($"/applications/{response.Id}", response);
    }

    [HttpPut]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateApplication([FromBody] UpdateApplicationCommand command)
    {
        var result = await _mediator.Send(command);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{applicationId}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteApplication([FromRoute] short applicationId)
    {
        var command = new DeleteApplicationCommand(applicationId);
        var result = await _mediator.Send(command);
        return result ? NoContent() : NotFound();
    }
}
