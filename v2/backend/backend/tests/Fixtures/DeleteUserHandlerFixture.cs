using System;
using System.Collections.Generic;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace tests.Fixtures;

public class DeleteUserHandlerFixture : IDisposable
{
    public ApplicationDbContext db;
    
    public DeleteUserHandlerFixture()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("StudySeekingDatabase")
            .Options;
        db = new ApplicationDbContext(options);
        
        InitializeUsers();
        db.SaveChanges();
    }
    
    public void Dispose()
    {
        db.Database.EnsureDeleted();
        db.Dispose();
    }

    private void InitializeUsers()
    {
        db.Users.AddRange( new List<User>()
            {
                new User() {Username = "nick", Email = "email"},
                new User() {Username = "admin", Email = "email"}
            }
        );
    }
}
