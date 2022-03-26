using System.Linq;
using System.Threading.Tasks;
using Api.Commands;
using Api.Handlers.Command;
using Xunit;

namespace Tests.Handlers.Command;

[Collection("UpdateApplicationCommandHandlerTests")]
public class UpdateApplicationCommandHandlerTests : HandlerTestsBase
{
    [Fact]
    public async Task UpdateApplicationCommandHandler_ShouldUpdateApplication_WhenApplicationExists()
    {
        // Arrange
        var command = new UpdateApplicationCommand()
            {Id = 1, Name = "newName", Subdomain = "newSubdomain"};
        var handler = new UpdateApplicationCommandHandler(Db, Mapper);
        // Act
        await handler.Handle(command, CancellationToken);
        var application = Db.Applications.First(a => a.Id == 1);
        // Assert
        Assert.Equal("newName", application.Name);
        Assert.Equal("newSubdomain", application.Subdomain);
    }

    [Fact]
    public async Task UpdateApplicationCommandHandler_ShouldReturnResponse_WhenApplicationExists()
    {
        // Arrange
        var command = new UpdateApplicationCommand()
            {Id = 1, Name = "newName", Subdomain = "newSubdomain"};
        var handler = new UpdateApplicationCommandHandler(Db, Mapper);
        // Act
        var response = await handler.Handle(command, CancellationToken);
        // Assert
        Assert.NotNull(response);
        Assert.Equal(1, response!.Id);
        Assert.Equal("newName", response.Name);
        Assert.Equal("newSubdomain", response.Subdomain);
    }

    [Fact]
    public async Task UpdateApplicationCommandHandler_ShouldReturnResponseWithErrors_WhenApplicationDoesNotExist()
    {
        // Arrange
        var command = new UpdateApplicationCommand()
            {Id = 200, Name = "fake", Subdomain = "fake"};
        var handler = new UpdateApplicationCommandHandler(Db, Mapper);
        // Act
        var response = await handler.Handle(command, CancellationToken);
        // Assert
        Assert.Contains(response.Errors, e => e.Contains("not found"));
    }

    [Fact]
    public async Task UpdateApplicationCommandHandler_ShouldNotUpdateApplication_WhenNameOrSubdomainIsTaken()
    {
        // Arrange
        var command = new UpdateApplicationCommand()
            {Id = 1, Name = "taken", Subdomain = "taken"};
        var handler = new UpdateApplicationCommandHandler(Db, Mapper);
        // Act
        await handler.Handle(command, CancellationToken);
        var application = Db.Applications.First(a => a.Id == 1);
        // Assert
        Assert.Equal("Computer Science", application.Name);
        Assert.Equal("cs", application.Subdomain);
        Assert.NotEqual("taken", application.Name);
        Assert.NotEqual("taken", application.Subdomain);
    }
    
    [Fact]
    public async Task UpdateApplicationCommandHandler_ShouldReturnResponseWithErrors_WhenNameOrSubdomainIsTaken()
    {
        // Arrange
        var command = new UpdateApplicationCommand()
            {Id = 1, Name = "taken", Subdomain = "taken"};
        var handler = new UpdateApplicationCommandHandler(Db, Mapper);
        // Act
        var response = await handler.Handle(command, CancellationToken);
        // Assert
        Assert.Contains(response.Errors, e => e.Contains("taken"));
    }
}
