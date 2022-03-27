using System.Threading;
using System.Threading.Tasks;
using Api.Commands;
using Api.Controllers;
using Api.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
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
    
    [Fact]
    public async Task AuthControllerSignin_ShouldReturnOk_WhenUserCredentialsAreValid()
    {
        // Arrange
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<AuthSigninCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new AuthSigninResponse() { RefreshToken = "token" }));
        
        var httpResponse = new Mock<HttpResponse>();
        var cookies = new Mock<IResponseCookies>();
        var httpContext = new Mock<HttpContext>();
        httpContext.SetupGet(c=> c.Response).Returns(httpResponse.Object);
        httpResponse.SetupGet(c => c.Cookies).Returns(cookies.Object);
        
        var controllerContext = new ControllerContext() {HttpContext = httpContext.Object};
        var controller = new AuthController(mediator.Object) { ControllerContext = controllerContext };
        // Act
        var response = await controller.Signin(It.IsAny<AuthSigninCommand>());
        // Assert
        Assert.IsType<OkObjectResult>(response);
    }
    
    [Fact]
    public async Task AuthControllerSignin_ShouldReturnBadRequest_WhenCredentialsAreNotValid()
    {
        // Arrange
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<AuthSigninCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new AuthSigninResponse{ Errors = { "foo", "bar", "baz" }}));
        var controller = new AuthController(mediator.Object);
        // Act
        var response = await controller.Signin(It.IsAny<AuthSigninCommand>());
        // Assert
        Assert.IsType<BadRequestObjectResult>(response);
    }

    [Fact]
    public void AuthControllerRefresh_ShouldReturnBadRequest_WhenRefreshTokenIsNotPresent()
    {
        // Cannot figure out how to mock cookies...
        // Seems like microsoft made this near impossible in .net core 3.1:
        // https://stackoverflow.com/questions/60445072/in-net-core-3-1-the-requestcookiecollection-can-no-longer-be-used-to-create-co
    }
}
