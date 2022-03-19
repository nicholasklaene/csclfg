using api.Data.EntityConfigurations;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) {}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ApplicationConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new TagConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryHasSuggestedTagConfiguration());
        modelBuilder.ApplyConfiguration(new PostConfiguration());
        modelBuilder.ApplyConfiguration(new PostHasTagConfiguration());
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Application> Applications => Set<Application>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Tag> Tags => Set<Tag>();

    public DbSet<CategoryHasSuggestedTag> CategoryHasSuggestedTags => Set<CategoryHasSuggestedTag>();

    public DbSet<Post> Posts => Set<Post>();

    public DbSet<PostHasTag> PostHasTags => Set<PostHasTag>();
}
