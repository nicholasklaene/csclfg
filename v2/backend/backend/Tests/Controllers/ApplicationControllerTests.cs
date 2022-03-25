using System.Threading;
using System.Threading.Tasks;
using Api.Commands;
using Api.Controllers;
using Api.Queries;
using Api.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Tests.Controllers;

public class ApplicationControllerTests
{
    [Fact]
    public async Task ApplicationControllerGetAllApplications_ShouldReturnOk_Always()
    {
        // Arrange
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<GetAllApplicationsQuery>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new GetAllApplicationsResponse()));
        var controller = new ApplicationController(mediator.Object);
        // Act
        var response = await controller.GetAllApplications();
        // Assert
        Assert.IsType<OkObjectResult>(response);
    }

    [Fact]
    public async Task ApplicationControllerGetApplicationById_ShouldReturnOk_WhenApplicationExists()
    {
        // Arrange
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<GetApplicationByIdQuery>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<GetApplicationByIdResponse?>(new GetApplicationByIdResponse()));
        var controller = new ApplicationController(mediator.Object);
        // Act
        var response = await controller.GetApplicationById(It.IsAny<short>());
        // Assert
        Assert.IsType<OkObjectResult>(response);
    }

    [Fact]
    public async Task ApplicationControllerGetApplicationById_ShouldReturnNotFound_WhenApplicationDoesNotExist()
    {
        // Arrange
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<GetApplicationByIdQuery>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<GetApplicationByIdResponse?>(null));
        var controller = new ApplicationController(mediator.Object);
        // Act
        var response = await controller.GetApplicationById(It.IsAny<short>());
        // Assert
        Assert.IsType<NotFoundResult>(response);
    }

    [Fact]
    public async Task ApplicationControllerCreateApplication_ShouldReturnCreated_WhenApplicationIsCreated()
    {
        // Arrange
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<CreateApplicationCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<CreateApplicationResponse?>(new CreateApplicationResponse()));
        var controller = new ApplicationController(mediator.Object);
        // Act
        var response = await controller.CreateApplication(It.IsAny<CreateApplicationCommand>());
        // Assert
        Assert.IsType<CreatedResult>(response);
    }
    
    [Fact]
    public async Task ApplicationControllerCreateApplication_ShouldReturnConflict_WhenApplicationIsNotCreated()
    {
        // Arrange
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<CreateApplicationCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<CreateApplicationResponse?>(null));
        var controller = new ApplicationController(mediator.Object);
        // Act
        var response = await controller.CreateApplication(It.IsAny<CreateApplicationCommand>());
        // Assert
        Assert.IsType<ConflictResult>(response);
    }
    
    [Fact]
    public async Task ApplicationControllerUpdateApplication_ShouldReturnOk_WhenApplicationIsUpdated()
    {
        // Arrange
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<UpdateApplicationCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new UpdateApplicationResponse()));
        var controller = new ApplicationController(mediator.Object);
        // Act
        var response = await controller.UpdateApplication(It.IsAny<UpdateApplicationCommand>());
        // Assert
        Assert.IsType<OkObjectResult>(response);
    }
    
    [Fact]
    public async Task ApplicationControllerUpdateApplication_ShouldReturnConflict_WhenApplicationAttributeIsTaken()
    {
        // Arrange
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<UpdateApplicationCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new UpdateApplicationResponse() { Errors = { "taken" }}));
        var controller = new ApplicationController(mediator.Object);
        // Act
        var response = await controller.UpdateApplication(It.IsAny<UpdateApplicationCommand>());
        // Assert
        Assert.IsType<ConflictObjectResult>(response);
    }
    
    [Fact]
    public async Task ApplicationControllerUpdateApplication_ShouldReturnNotFound_WhenApplicationIsNotFound()
    {
        // Arrange
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<UpdateApplicationCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new UpdateApplicationResponse() { Errors = { "not found" }}));
        var controller = new ApplicationController(mediator.Object);
        // Act
        var response = await controller.UpdateApplication(It.IsAny<UpdateApplicationCommand>());
        // Assert
        Assert.IsType<NotFoundResult>(response);
    }

    [Fact]
    public async Task ApplicationControllerDeleteApplication_ShouldReturnNoContent_WhenApplicationIsDeleted()
    {
        // Arrange
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<DeleteApplicationCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(true));
        var controller = new ApplicationController(mediator.Object);
        // Act
        var response = await controller.DeleteApplication(It.IsAny<short>());
        // Assert
        Assert.IsType<NoContentResult>(response);
    }

    [Fact]
    public async Task ApplicationControllerDeleteApplication_ShouldReturnNotFound_WhenApplicationIsNotFound()
    {
        // Arrange
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<DeleteApplicationCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(false));
        var controller = new ApplicationController(mediator.Object);
        // Act
        var response = await controller.DeleteApplication(It.IsAny<short>());
        // Assert
        Assert.IsType<NotFoundResult>(response);
    }
}
