using System.Threading;
using System.Threading.Tasks;
using Api.Commands;
using Api.Controllers;
using Api.Queries;
using Api.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using Xunit;

namespace Tests.Controllers;

public class AuthControllerTests
{
    [Fact]
    public async Task AuthControllerSignup_ShouldReturnStatusCode201_WhenUserIsCreated()
    {
        // Arrange
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<AuthSignupCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new AuthSignupResponse()));
        var controller = new AuthController(mediator.Object);
        // Act
        var response = await controller.Signup(It.IsAny<AuthSignupCommand>());
        var statusCodeResult = (IStatusCodeActionResult) response;
        // Assert
        Assert.IsType<StatusCodeResult>(response);
        Assert.Equal(201, statusCodeResult.StatusCode);
    }
    
    [Fact]
    public async Task AuthControllerSignup_ShouldReturnConflict_WhenUserIsNotCreated()
    {
        // Arrange
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<AuthSignupCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new AuthSignupResponse() { Errors = { "foo", "bar", "baz" }}));
        var controller = new AuthController(mediator.Object);
        // Act
        var response = await controller.Signup(It.IsAny<AuthSignupCommand>());
        // Assert
        Assert.IsType<ConflictObjectResult>(response);
    }
}
