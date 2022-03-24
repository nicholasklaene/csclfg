using System.Linq;
using System.Threading;
using api.Commands;
using api.Handlers;
using api.Mappings;
using AutoMapper;
using tests.Fixtures;
using Xunit;

namespace tests.Handlers;

public class CreateApplicationHandlerTests : IClassFixture<CreateApplicationHandlerFixture>
{
    private readonly IMapper _mapper;
    private readonly CreateApplicationHandlerFixture _fixture;
    private readonly CancellationToken _cancellationToken;
    
    public CreateApplicationHandlerTests(CreateApplicationHandlerFixture fixture)
    {
        _cancellationToken = new CancellationToken();
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new ApplicationProfile());
        });
        _mapper = mappingConfig.CreateMapper();
        _fixture = fixture;
    }

    [Fact]
    public void CreateApplicationHandler_ShouldCreateApplication_IfApplicationDoesNotExist()
    {
        // Arrange
        var command = new CreateApplicationCommand() { Name = "newApp", Subdomain = "new" };
        var handler = new CreateApplicationHandler(_fixture.Db, _mapper);
        var originalCount = _fixture.Db.Applications.Count();
        // Act
        handler.Handle(command, _cancellationToken);
        // Assert
        Assert.Contains(_fixture.Db.Applications, a => a.Name == "newApp");
        Assert.NotEqual(originalCount, _fixture.Db.Applications.Count());
    }

    [Fact]
    public void CreateApplicationHandler_ShouldNotCreateApplication_IfApplicationExists()
    {
        // Arrange
        var command = new CreateApplicationCommand(){ Name = "existingApp", Subdomain = "exists"};
        var handler = new CreateApplicationHandler(_fixture.Db, _mapper);
        var originalCount = _fixture.Db.Applications.Count();
        // Act
        handler.Handle(command, _cancellationToken);
        // Assert
        Assert.Equal(originalCount, _fixture.Db.Applications.Count());
        Assert.Contains(_fixture.Db.Applications, a => a.Name == "existingApp");
    }
}
