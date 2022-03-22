using System.Collections.Generic;
using System.Threading;
using api.Commands;
using api.Data;
using api.Handlers;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace tests.Handlers;

public class DeleteUserHandlerTest
{
    [Fact] 
    public void Test()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "studyseeking")
            .Options;
        var db = new ApplicationDbContext(options);
        
        db.Users.AddRange( new List<User>()
            {
                new User() {Username = "nick", Email = "email"},
                new User() {Username = "admin", Email = "email"}
            }
        );
        db.SaveChanges();

        Assert.Contains(db.Users, u => u.Username == "nick");
        
        var cancellationToken = new CancellationToken();
        var command = new DeleteUserCommand("nick");
        var handler = new DeleteUserHandler(db);
        handler.Handle(command, cancellationToken);
        
        Assert.DoesNotContain(db.Users, u => u.Username == "nick");
        Assert.NotNull(db);
    }
}