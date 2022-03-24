using System;
using System.Threading;
using api.Data;
using api.Mappings;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace tests.Handlers;

public class HandlerTestsBase : IDisposable
{
    protected readonly IMapper Mapper;
    protected readonly ApplicationDbContext Db;
    protected readonly CancellationToken CancellationToken = new CancellationToken();

    protected HandlerTestsBase()
    {
        var options  = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: new Guid().ToString())
            .Options;
        Db = new ApplicationDbContext(options);
        Db.Database.EnsureCreated();
        
        DatabaseInitializer.Initialize(Db);
        
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new ApplicationProfile());
        });
        Mapper = mappingConfig.CreateMapper();;
    }
    
    public void Dispose()
    {
        Db.Database.EnsureDeleted();
        Db.Dispose();
        GC.SuppressFinalize(this);
    }
}
