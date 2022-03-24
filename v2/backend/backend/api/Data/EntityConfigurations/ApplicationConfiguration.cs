using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.EntityConfigurations;

public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
{
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        builder.ToTable("applications");
        
        builder.HasKey(c => c.Id);
        
        builder
            .HasMany(a => a.Categories)
            .WithOne(c => c.Application);

        builder
            .HasIndex(a => a.Name)
            .IsUnique();

        builder
            .HasIndex(a => a.Subdomain)
            .IsUnique();
        
        builder
            .Property(a => a.Id)
            .HasColumnName("id");
        builder
            .Property(a => a.Name)
            .IsRequired()
            .HasColumnName("name")
            .HasMaxLength(50);
        builder
            .Property(a => a.Subdomain)
            .IsRequired()
            .HasColumnName("subdomain")
            .HasMaxLength(15);
    }
}