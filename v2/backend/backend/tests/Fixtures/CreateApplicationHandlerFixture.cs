using System;
using System.Collections.Generic;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace tests.Fixtures;

public class CreateApplicationHandlerFixture : IDisposable
{
    public readonly ApplicationDbContext Db;
    
    public CreateApplicationHandlerFixture()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("StudySeekingDatabase")
            .Options;
        Db = new ApplicationDbContext(options);
        
        InitializeApplications();
        Db.SaveChanges();
    }
    
    public void Dispose()
    {
        Db.Database.EnsureDeleted();
        Db.Dispose();
    }

    private void InitializeApplications()
    {
        Db.Applications.Add(new Application() { Id = 1, Name = "existingApp", Subdomain = "existing" });
    }
}
