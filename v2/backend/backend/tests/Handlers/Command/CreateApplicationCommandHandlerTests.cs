using System.Linq;
using System.Threading.Tasks;
using api.Commands;
using api.Handlers.Command;
using Xunit;

namespace tests.Handlers.Command;

[Collection("CreateApplicationHandlerTests")]
public class CreateApplicationHandlerTests : HandlerTestsBase
{
    [Fact]
    public async Task CreateApplicationCommandHandler_ShouldCreateApplication_WhenApplicationIsValid()
    {
        // Arrange
        var command = new CreateApplicationCommand() { Name = "New Application", Subdomain = "new" };
        var handler = new CreateApplicationCommandHandler(Db, Mapper);
        // Act
        await handler.Handle(command, CancellationToken);
        // Assert
        Assert.Contains(Db.Applications, a => a.Name == "New Application" && a.Subdomain == "new");
    }
    
    [Fact]
    public async Task CreateApplicationCommandHandler_ShouldReturnResponse_WhenApplicationIsValid()
    {
        // Arrange
        var command = new CreateApplicationCommand() { Name = "New Application", Subdomain = "new" };
        var handler = new CreateApplicationCommandHandler(Db, Mapper);
        // Act
        var response = await handler.Handle(command, CancellationToken);
        // Assert
        Assert.NotNull(response);
        Assert.Equal("New Application", response!.Name);
        Assert.Equal("new", response.Subdomain);
    }

    [Fact]
    public async Task CreateApplicationCommandHandler_ShouldNotCreateApplication_WhenApplicationExists()
    {
        // Arrange
        var command = new CreateApplicationCommand() { Name = "Computer Science", Subdomain = "cs" };
        var handler = new CreateApplicationCommandHandler(Db, Mapper);
        var numApplications = Db.Applications.Count();
        // Act
        await handler.Handle(command, CancellationToken);
        // Assert
        Assert.Equal(numApplications, Db.Applications.Count());
    }
    
    [Fact]
    public async Task CreateApplicationCommandHandler_ShouldNotReturnResponse_WhenApplicationExists()
    {
        // Arrange
        var command = new CreateApplicationCommand() { Name = "Computer Science", Subdomain = "cs" };
        var handler = new CreateApplicationCommandHandler(Db, Mapper);
        // Act
        var response = await handler.Handle(command, CancellationToken);
        // Assert
        Assert.Null(response);
    }
}
