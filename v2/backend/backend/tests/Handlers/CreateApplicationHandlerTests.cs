using System;
using System.Linq;
using System.Threading;
using api.Commands;
using api.Data;
using api.Handlers;
using api.Mappings;
using api.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using tests.Fixtures;
using Xunit;

namespace tests.Handlers;

public class CreateApplicationHandlerTests : IClassFixture<CreateApplicationHandlerFixture>
{
    IMapper mapper;
    CreateApplicationHandlerFixture fixture;
    CancellationToken cancellationToken;
    
    public CreateApplicationHandlerTests(CreateApplicationHandlerFixture fixture)
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new ApplicationProfile());
        });
        mapper = mappingConfig.CreateMapper();
        this.fixture = fixture;
    }

    [Fact]
    public void CreateApplicationHandler_ShouldCreateApplication_IfApplicationDoesNotExist()
    {
        // Arrange
        var command = new CreateApplicationCommand() { Name = "newApp", Subdomain = "new" };
        var handler = new CreateApplicationHandler(fixture.db, mapper);
        var originalCount = fixture.db.Applications.Count();
        // Act
        handler.Handle(command, cancellationToken);
        // Assert
        Assert.Contains(fixture.db.Applications, a => a.Name == "newApp");
        Assert.NotEqual(originalCount, fixture.db.Applications.Count());
    }

    [Fact]
    public void CreateApplicationHandler_ShouldNotCreateApplication_IfApplicationExists()
    {
        // Arrange
        var command = new CreateApplicationCommand(){ Name = "existingApp", Subdomain = "exists"};
        var handler = new CreateApplicationHandler(fixture.db, mapper);
        
        var originalCount = fixture.db.Applications.Count();
        // Act
        handler.Handle(command, cancellationToken);
        // Assert
        Assert.Equal(originalCount, fixture.db.Applications.Count());
        Assert.Contains(fixture.db.Applications, a => a.Name == "existingApp");
    }
}
