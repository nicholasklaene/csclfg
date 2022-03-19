using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("/api/users")]
public class UserController : ControllerBase
{
    [HttpDelete("/{username}")]
    public async Task<IActionResult> DeleteUser([FromRoute] string username)
    {
        return Ok();
    }
}
