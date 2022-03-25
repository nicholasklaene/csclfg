using Api.Commands;
using Xunit;

namespace Tests.Commands;

public class CreateApplicationCommandTests : CommandTestsBase
{
    [Fact]
    public void CreateApplicationCommand_ShouldBeValid_WhenFieldsAreValid()
    {
        // Arrange
        var command1 = new CreateApplicationCommand {  Name = "bla", Subdomain = "bla" };
        var command2 = new CreateApplicationCommand {  Name = "hello", Subdomain = "world" };
        var command3 = new CreateApplicationCommand {  Name = "name", Subdomain = "15characters15c"};
        // Act
        var command1Validation = ValidateModel(command1);
        var command2Validation = ValidateModel(command2);
        var command3Validation = ValidateModel(command3);
        // Assert
        Assert.Empty(command1Validation);
        Assert.Empty(command2Validation);
        Assert.Empty(command3Validation);
    }

    [Fact]
    public void CreateApplicationCommand_ShouldNotBeValid_WhenMaximumFieldLengthExceeded()
    {
        // Arrange
        var command1 = new CreateApplicationCommand() { 
            Name = 
                "50CharactersExceeded50CharactersExceeded50CharactersExceeded50CharactersExceeded50CharactersExceeded",
            Subdomain = "normal" 
        };
        var command2 = new CreateApplicationCommand()
        {
             Name = "hello", Subdomain = "15CharactersExceeded15CharactersExceeded"
        };
        // Act
        var command1Validation = ValidateModel(command1);
        var command2Validation = ValidateModel(command2);
        // Assert
        Assert.NotEmpty(command1Validation);
        Assert.NotEmpty(command2Validation);

        Assert.True(ValidationHasErrorWithMessage(command1Validation, "Name"));
        Assert.False(ValidationHasErrorWithMessage(command1Validation, "Subdomain"));

        Assert.True(ValidationHasErrorWithMessage(command2Validation, "Subdomain"));
        Assert.False(ValidationHasErrorWithMessage(command2Validation, "Name"));
    }
    
    [Fact]
    public void CreateApplicationCommand_ShouldNotBeValid_WhenAFieldIsMissing()
    {
        // Arrange
        var command1 = new CreateApplicationCommand() { Name = "", Subdomain = null! };
        var command2 = new CreateApplicationCommand() { Name = "hello", Subdomain = null! };
        var command3 = new CreateApplicationCommand();
        // Act
        var command1Validation = ValidateModel(command1);
        var command2Validation = ValidateModel(command2);
        var command3Validation = ValidateModel(command3);
        // Assert
        Assert.NotEmpty(command1Validation);
        Assert.NotEmpty(command2Validation);
        Assert.NotEmpty(command3Validation);
        
        Assert.True(ValidationHasErrorWithMessage(command1Validation, "Name"));
        Assert.True(ValidationHasErrorWithMessage(command1Validation, "Subdomain"));
        
        Assert.True(ValidationHasErrorWithMessage(command2Validation, "Subdomain"));
        Assert.False(ValidationHasErrorWithMessage(command2Validation, "Name"));
        
        Assert.True(ValidationHasErrorWithMessage(command3Validation, "Name"));
        Assert.True(ValidationHasErrorWithMessage(command3Validation, "Subdomain"));
    }
}
