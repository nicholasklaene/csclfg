using System;
using System.Collections.Generic;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace tests.Fixtures;

public class CreateApplicationHandlerFixture : IDisposable
{
    public ApplicationDbContext db;
    
    public CreateApplicationHandlerFixture()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("StudySeekingDatabase")
            .Options;
        db = new ApplicationDbContext(options);
        
        InitializeApplications();
        db.SaveChanges();
    }
    
    public void Dispose()
    {
        db.Database.EnsureDeleted();
        db.Dispose();
    }

    private void InitializeApplications()
    {
        db.Applications.Add(new Application() { Id = 1, Name = "existingApp", Subdomain = "existing" });
    }
}
