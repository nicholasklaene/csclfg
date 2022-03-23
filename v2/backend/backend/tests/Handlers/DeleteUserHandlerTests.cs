using System.Linq;
using System.Threading;
using api.Commands;
using api.Handlers;
using tests.Fixtures;
using Xunit;

namespace tests.Handlers;

public class DeleteUserHandlerTests : IClassFixture<DeleteUserHandlerFixture>
{
    DeleteUserHandlerFixture fixture;
    CancellationToken cancellationToken;
    
    public DeleteUserHandlerTests(DeleteUserHandlerFixture fixture)
    {
        cancellationToken = new CancellationToken();
        this.fixture = fixture;
    }

    [Fact] 
    public void DeleteUserHandler_ShouldDeleteUser_IfUserExists()
    {
        // Arrange
        var command = new DeleteUserCommand("nick");
        var handler = new DeleteUserHandler(fixture.db);
        // Act
        handler.Handle(command, cancellationToken);
        // Assert
        Assert.DoesNotContain(fixture.db.Users, u => u.Username == "nick");
        Assert.Contains(fixture.db.Users, u => u.Username == "admin");
    }

    [Fact]
    public void DeleteUserHandler_ShouldNotDeleteUser_IfUserDoesNotExist()
    {
        // Arrange
        var command = new DeleteUserCommand("fakeUser");
        var handler = new DeleteUserHandler(fixture.db);
        var numUsers = fixture.db.Users.Count();
        // Act
        handler.Handle(command, cancellationToken);
        // Assert
        Assert.DoesNotContain(fixture.db.Users, u => u.Username == "fakeUser");
        Assert.Equal(numUsers, fixture.db.Users.Count());
    }
}
