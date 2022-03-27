using System.Threading;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider.Model;
using Api.Commands;
using Api.Handlers.Command;
using Moq;
using Xunit;

namespace Tests.Handlers.Command;

[Collection("AuthSignupCommandHandlerTests")]
public class AuthSignupCommandHandlerTests : HandlerTestsBase
{
    [Fact]
    public async Task AuthSignUpCommandHandler_ShouldReturnResponseWithNoErrors_WhenUserIsValid()
    {
        // Arrange
        var authSignupCommand = new AuthSignupCommand()
            { Username = "someRandomUser", Password = "password", Email = "hello@world.com" };
        IdentityClientMock.Setup(i => i.SignUpAsync(It.IsAny<SignUpRequest>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new SignUpResponse()));
        var handler = new AuthSignupCommandHandler(Configuration, Db, IdentityClientMock.Object);
        // Act
        var response = await handler.Handle(authSignupCommand, CancellationToken);
        // Assert
        Assert.Empty(response.Errors);
    }
    
    [Fact]
    public async Task AuthSignUpCommandHandler_ShouldCreateUserInDb_WhenUserIsValid()
    {
        // Arrange
        var authSignupCommand = new AuthSignupCommand()
            { Username = "someRandomUser", Password = "password", Email = "hello@world.com" };
        IdentityClientMock.Setup(i => i.SignUpAsync(It.IsAny<SignUpRequest>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new SignUpResponse()));
        var handler = new AuthSignupCommandHandler(Configuration, Db, IdentityClientMock.Object);
        // Act
        await handler.Handle(authSignupCommand, CancellationToken);
        // Assert
        Assert.Contains(Db.Users, u => u.Username == "someRandomUser");
    }
    
    [Fact]
    public async Task AuthSignUpCommandHandler_ShouldReturnResponseWithErrors_WhenUserIsNotValid()
    {
        // Arrange
        var authSignupCommand = new AuthSignupCommand()
            { Username = "someRandomUser", Password = "password", Email = "hello@world.com" };
        IdentityClientMock.Setup(i => i.SignUpAsync(It.IsAny<SignUpRequest>(), It.IsAny<CancellationToken>()))
            .Throws(new UsernameExistsException("Exception occured"));
        var handler = new AuthSignupCommandHandler(Configuration, Db, IdentityClientMock.Object);
        // Act
        var response = await handler.Handle(authSignupCommand, CancellationToken);
        // Assert
        Assert.NotEmpty(response.Errors);
    }
    
    [Fact]
    public async Task AuthSignUpCommandHandler_ShouldNotCreateUser_WhenUserIsNotValid()
    {
        // Arrange
        var authSignupCommand = new AuthSignupCommand()
            { Username = "someRandomUser", Password = "password", Email = "hello@world.com" };
        IdentityClientMock.Setup(i => i.SignUpAsync(It.IsAny<SignUpRequest>(), It.IsAny<CancellationToken>()))
            .Throws(new UsernameExistsException("Exception occured"));
        var handler = new AuthSignupCommandHandler(Configuration, Db, IdentityClientMock.Object);
        // Act
        await handler.Handle(authSignupCommand, CancellationToken);
        // Assert
        Assert.DoesNotContain(Db.Users, u => u.Username == "someRandomUser");
    }
}
