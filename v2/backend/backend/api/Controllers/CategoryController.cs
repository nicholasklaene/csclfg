using api.Commands;
using api.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("/categories")]
public class CategoryController : ControllerBase
{

    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetApplicationCategories([FromQuery] short applicationId)
    {
        var query = new GetApplicationCategoriesQuery(applicationId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetCategoryById([FromRoute] int categoryId)
    {
        var query = new GetCategoryByIdQuery(categoryId);
        var result = await _mediator.Send(query);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
    {
        var result = await _mediator.Send(command);
        return result != null ? Created($"/categories/{result.Id}", result): BadRequest();
    }

    [HttpPut]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommand command)
    {
        var result = await _mediator.Send(command);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpDelete("{categoryId}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteCategory([FromRoute] int categoryId)
    {
        var command = new DeleteCategoryCommand(categoryId);
        var result = await _mediator.Send(command);
        return result ? NoContent() : NotFound();
    }
}
