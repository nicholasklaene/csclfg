using Api.Commands;
using Api.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetApplicationById([FromRoute] short id)
    {
        var query = new GetApplicationByIdQuery(id);
        var result = await _mediator.Send(query);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateApplication([FromBody] CreateApplicationCommand command)
    {
        var response = await _mediator.Send(command);
        return response is null ? Conflict() : Created($"/applications/{response.Id}", response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateApplication([FromBody] UpdateApplicationCommand command)
    {
        var response = await _mediator.Send(command);
        if (response.Errors.Count == 0) return Ok(response);
        var notFound = response.Errors.Exists(e => e.Contains("not found"));
        return notFound ? NotFound() : Conflict(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteApplication([FromRoute] short id)
    {
        var command = new DeleteApplicationCommand(id);
        var result = await _mediator.Send(command);
        return result ? NoContent() : NotFound();
    }
}
