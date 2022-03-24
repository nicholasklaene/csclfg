using System.Linq;
using System.Threading;
using api.Commands;
using api.Handlers;
using tests.Fixtures;
using Xunit;

namespace tests.Handlers;

public class DeleteUserHandlerTests : IClassFixture<DeleteUserHandlerFixture>
{
    private readonly DeleteUserHandlerFixture _fixture;
    private readonly CancellationToken _cancellationToken;
    
    public DeleteUserHandlerTests(DeleteUserHandlerFixture fixture)
    {
        _cancellationToken = new CancellationToken();
        _fixture = fixture;
    }

    [Fact] 
    public void DeleteUserHandler_ShouldDeleteUser_IfUserExists()
    {
        // Arrange
        var command = new DeleteUserCommand("nick");
        var handler = new DeleteUserHandler(_fixture.Db);
        // Act
        handler.Handle(command, _cancellationToken);
        // Assert
        Assert.DoesNotContain(_fixture.Db.Users, u => u.Username == "nick");
        Assert.Contains(_fixture.Db.Users, u => u.Username == "admin");
    }

    [Fact]
    public void DeleteUserHandler_ShouldNotDeleteUser_IfUserDoesNotExist()
    {
        // Arrange
        var command = new DeleteUserCommand("fakeUser");
        var handler = new DeleteUserHandler(_fixture.Db);
        var numUsers = _fixture.Db.Users.Count();
        // Act
        handler.Handle(command, _cancellationToken);
        // Assert
        Assert.DoesNotContain(_fixture.Db.Users, u => u.Username == "fakeUser");
        Assert.Equal(numUsers, _fixture.Db.Users.Count());
    }
}
