using Api.Commands;
using Xunit;

namespace Tests.Commands;

public class UpdateApplicationCommandTests : CommandTestsBase
{
    [Fact]
    public void UpdateApplicationCommand_ShouldBeValid_WhenFieldsAreValid()
    {
        // Arrange
        var command1 = new UpdateApplicationCommand { Id = 1, Name = "bla", Subdomain = "bla" };
        var command2 = new UpdateApplicationCommand { Id = 2, Name = "hello", Subdomain = "world" };
        var command3 = new UpdateApplicationCommand { Id = 3, Name = "name", Subdomain = "15characters15c"};
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
    public void UpdateApplicationCommand_ShouldNotBeValid_WhenMaximumFieldLengthExceeded()
    {
        // Arrange
        var command1 = new UpdateApplicationCommand() { 
            Id = 1, Name = 
                "50CharactersExceeded50CharactersExceeded50CharactersExceeded50CharactersExceeded50CharactersExceeded",
            Subdomain = "normal" 
        };
        var command2 = new UpdateApplicationCommand()
        {
            Id = 50, Name = "hello", Subdomain = "15CharactersExceeded15CharactersExceeded"
        };
        // Act
        var command1Validation = ValidateModel(command1);
        var command2Validation = ValidateModel(command2);
        // Assert
        Assert.NotEmpty(command1Validation);
        Assert.NotEmpty(command2Validation);

        Assert.True(ValidationHasErrorWithMessage(command1Validation, "Name"));
        Assert.False(ValidationHasErrorWithMessage(command1Validation, "Subdomain"));
        Assert.False(ValidationHasErrorWithMessage(command1Validation, "Id"));
        
        Assert.True(ValidationHasErrorWithMessage(command2Validation, "Subdomain"));
        Assert.False(ValidationHasErrorWithMessage(command2Validation, "Name"));
        Assert.False(ValidationHasErrorWithMessage(command2Validation, "Id"));
    }
    
    [Fact]
    public void UpdateApplicationCommand_ShouldNotBeValid_WhenAFieldIsMissing()
    {
        // Arrange
        var command1 = new UpdateApplicationCommand() { Id = 1, Name = "", Subdomain = null! };
        var command2 = new UpdateApplicationCommand() { Name = "hello", Subdomain = null! };
        var command3 = new UpdateApplicationCommand();
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
        Assert.False(ValidationHasErrorWithMessage(command1Validation, "Id"));
        
        Assert.True(ValidationHasErrorWithMessage(command2Validation, "Id"));
        Assert.True(ValidationHasErrorWithMessage(command2Validation, "Subdomain"));
        Assert.False(ValidationHasErrorWithMessage(command2Validation, "Name"));
        
        Assert.True(ValidationHasErrorWithMessage(command3Validation, "Id"));
        Assert.True(ValidationHasErrorWithMessage(command3Validation, "Name"));
        Assert.True(ValidationHasErrorWithMessage(command3Validation, "Subdomain"));
    }
}
