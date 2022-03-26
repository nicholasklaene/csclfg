using System.Linq;
using System.Threading.Tasks;
using Api.Commands;
using Api.Handlers.Command;
using Xunit;

namespace Tests.Handlers.Command;

[Collection("DeleteApplicationCommandHandlerTests")]
public class DeleteApplicationCommandHandlerTests : HandlerTestsBase
{
    [Fact]
    public async Task DeleteApplicationCommandHandler_ShouldDeleteApplication_IfApplicationExists()
    {
        // Arrange
        var command = new DeleteApplicationCommand(1);
        var handler = new DeleteApplicationCommandHandler(Db);
        var startAppCount = Db.Applications.Count();
        // Act
        await handler.Handle(command, CancellationToken);
        // Assert
        Assert.NotEqual(startAppCount, Db.Applications.Count());
        Assert.DoesNotContain(Db.Applications, a => a.Id == 1);
    }

    [Fact]
    public async Task DeleteApplicationCommandHandler_ShouldNotDeleteApplication_IfApplicationDoesNotExist()
    {
        // Arrange
        var command = new DeleteApplicationCommand(200);
        var handler = new DeleteApplicationCommandHandler(Db);
        var appCount = Db.Applications.Count();
        // Act
        await handler.Handle(command, CancellationToken);
        // Assert
        Assert.Equal(appCount, Db.Applications.Count());
    }
    
    [Fact]
    public async Task DeleteApplicationCommandHandler_ShouldReturnTrue_IfApplicationExists()
    {
        // Arrange
        var command = new DeleteApplicationCommand(1);
        var handler = new DeleteApplicationCommandHandler(Db);
        // Act
        var result = await handler.Handle(command, CancellationToken);
        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteApplicationCommandHandler_ShouldReturnFalse_IfApplicationDoesNotExist()
    {
        // Arrange
        var command = new DeleteApplicationCommand(200);
        var handler = new DeleteApplicationCommandHandler(Db);
        // Act
        var result = await handler.Handle(command, CancellationToken);
        // Assert
        Assert.False(result);
    }
}
