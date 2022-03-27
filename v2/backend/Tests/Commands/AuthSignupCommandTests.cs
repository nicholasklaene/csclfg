using Api.Commands;
using Xunit;

namespace Tests.Commands;

public class AuthSignupCommandTests : CommandTestsBase
{
    [Fact]
    public void AuthSignupCommand_ShouldBeValid_WhenCommandIsValid()
    {
        // Arrange 
        var command = new AuthSignupCommand()
            {Username = "nick", Password = "password", Email = "someEmail@gmail.com"};
        // Act
        var commandValidation = ValidateModel(command);
        // Assert
        Assert.Empty(commandValidation);
    }

    [Fact]
    public void AuthSignupCommand_ShouldNotBeValid_WhenAFieldIsMissing()
    {
        // Arrange
        var command1 = new AuthSignupCommand()
            { Username = "nick", Password = "password" };
        var command2 = new AuthSignupCommand()
            { Username = "nick", Email = "someEmail@gmail.com" };
        var command3 = new AuthSignupCommand()
            {  Password = "password", Email = "someEmail@gmail.com" };
        var command4 = new AuthSignupCommand();
        // Act
        var command1Validation = ValidateModel(command1);
        var command2Validation = ValidateModel(command2);
        var command3Validation = ValidateModel(command3);
        var command4Validation = ValidateModel(command4);
        // Assert
        Assert.NotEmpty(command1Validation);
        Assert.NotEmpty(command2Validation);
        Assert.NotEmpty(command3Validation);
        Assert.NotEmpty(command4Validation);
        
        Assert.True(ValidationHasErrorWithMessage(command1Validation, "Email"));
        Assert.False(ValidationHasErrorWithMessage(command1Validation, "Username"));
        Assert.False(ValidationHasErrorWithMessage(command1Validation, "Password"));

        Assert.True(ValidationHasErrorWithMessage(command2Validation, "Password"));
        Assert.False(ValidationHasErrorWithMessage(command2Validation, "Username"));
        Assert.False(ValidationHasErrorWithMessage(command2Validation, "Email"));
        
        Assert.True(ValidationHasErrorWithMessage(command3Validation, "Username"));
        Assert.False(ValidationHasErrorWithMessage(command3Validation, "Email"));
        Assert.False(ValidationHasErrorWithMessage(command3Validation, "Password"));
        
        Assert.True(ValidationHasErrorWithMessage(command4Validation, "Username"));
        Assert.True(ValidationHasErrorWithMessage(command4Validation, "Email"));
        Assert.True(ValidationHasErrorWithMessage(command4Validation, "Password"));
    }

    [Fact]
    public void AuthSignupCommand_ShouldNotBeValid_WhenLengthConstraintsViolated()
    {
        // Arrange
        var superLongString = "";
        for (var i = 0; i < 100; i++) superLongString += "a";
        var command1 = new AuthSignupCommand()
            { Username = superLongString, Password = "password", Email = "someEmail@gmail.com"};
        var command2 = new AuthSignupCommand()
            { Username = "real_username", Password = superLongString, Email = "someEmail@gmail.com"};
        var command3 = new AuthSignupCommand()
            { Username = "real_username", Password = "password", Email = superLongString};
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
        
        Assert.True(ValidationHasErrorWithMessage(command3Validation, "Email"));
        Assert.False(ValidationHasErrorWithMessage(command3Validation, "Username"));
        Assert.False(ValidationHasErrorWithMessage(command3Validation, "Password"));
    }
}
