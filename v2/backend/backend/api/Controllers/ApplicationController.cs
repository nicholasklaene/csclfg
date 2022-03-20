using api.Commands;
using api.Queries;
using MediatR;
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
    public async Task<IActionResult> CreateApplication([FromBody] CreateApplicationCommand command)
    {
        var response = await _mediator.Send(command);
        return response == null ? StatusCode(500) : Created($"/applications/{response.Id}", response);
    }
}
