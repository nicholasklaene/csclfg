using System;
using System.Collections.Generic;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace tests.Fixtures;

public class DeleteUserHandlerFixture : IDisposable
{
    public readonly ApplicationDbContext Db;
    
    public DeleteUserHandlerFixture()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("StudySeekingDatabase")
            .Options;
        Db = new ApplicationDbContext(options);
        
        InitializeUsers();
        Db.SaveChanges();
    }
    
    public void Dispose()
    {
        Db.Database.EnsureDeleted();
        Db.Dispose();
    }

    private void InitializeUsers()
    {
        Db.Users.AddRange( new List<User>()
            {
                new User() { Username = "nick", Email = "email"},
                new User() { Username = "admin", Email = "email"},
            }
        );
    }
}
