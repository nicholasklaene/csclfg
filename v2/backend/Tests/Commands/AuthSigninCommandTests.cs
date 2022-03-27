using Api.Commands;
using Xunit;

namespace Tests.Commands;

public class AuthSigninCommandTests : CommandTestsBase
{
    [Fact]
    public void AuthSigninCommand_ShouldBeValid_WhenCommandIsValid()
    {
        // Arrange 
        var command = new AuthSigninCommand()
            { Username = "nick", Password = "password" };
        // Act
        var commandValidation = ValidateModel(command);
        // Assert
        Assert.Empty(commandValidation);
    }

    [Fact]
    public void AuthSigninCommand_ShouldNotBeValid_WhenAFieldIsMissing()
    {
        // Arrange
        var command1 = new AuthSigninCommand { Password = "password" };
        var command2 = new AuthSigninCommand { Username = "nick" };
        var command3 = new AuthSigninCommand();
        // Act
        var command1Validation = ValidateModel(command1);
        var command2Validation = ValidateModel(command2);
        var command3Validation = ValidateModel(command3);
        // Assert
        Assert.NotEmpty(command1Validation);
        Assert.NotEmpty(command2Validation);
        Assert.NotEmpty(command3Validation);
        
        Assert.True(ValidationHasErrorWithMessage(command1Validation, "Username"));
        Assert.False(ValidationHasErrorWithMessage(command1Validation, "Password"));
        Assert.True(ValidationHasErrorWithMessage(command2Validation, "Password"));
        Assert.False(ValidationHasErrorWithMessage(command2Validation, "Username"));

        Assert.True(ValidationHasErrorWithMessage(command3Validation, "Username"));
        Assert.True(ValidationHasErrorWithMessage(command3Validation, "Password"));
    }

    [Fact]
    public void AuthSigninCommand_ShouldNotBeValid_WhenLengthConstraintsViolated()
    {
        // Arrange
        var superLongString = "";
        for (var i = 0; i < 100; i++) superLongString += "a";
        var command1 = new AuthSigninCommand()
            { Username = superLongString, Password = "password" };
        var command2 = new AuthSigninCommand()
            { Username = "real_username", Password = superLongString };
        var command3 = new AuthSigninCommand()
            { Username = "real_username", Password = "short" };
        // Act
        var command1Validation = ValidateModel(command1);
        var command2Validation = ValidateModel(command2);
        var command3Validation = ValidateModel(command3);
        // Assert
        Assert.NotEmpty(command1Validation);
        Assert.NotEmpty(command2Validation);
        Assert.NotEmpty(command3Validation);
        
        Assert.True(ValidationHasErrorWithMessage(command1Validation, "Username"));
        Assert.False(ValidationHasErrorWithMessage(command1Validation, "Password"));
        Assert.False(ValidationHasErrorWithMessage(command1Validation, "Email"));

        Assert.True(ValidationHasErrorWithMessage(command2Validation, "Password"));
        Assert.False(ValidationHasErrorWithMessage(command2Validation, "Username"));
        Assert.False(ValidationHasErrorWithMessage(command2Validation, "Email"));
        
        Assert.True(ValidationHasErrorWithMessage(command3Validation, "Password"));
        Assert.False(ValidationHasErrorWithMessage(command3Validation, "Username"));
    }
}