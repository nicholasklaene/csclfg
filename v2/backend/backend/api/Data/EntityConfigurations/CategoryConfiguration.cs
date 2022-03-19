using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.EntityConfigurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");
        
        builder.HasKey(c => c.Id);

        builder
            .HasOne(c => c.Application)
            .WithMany(a => a.Categories)
            .HasForeignKey(c => c.ApplicationId);

        builder
            .Property(c => c.Id)
            .HasColumnName("id");
        builder
            .Property(c => c.ApplicationId)
            .HasColumnName("application_id");
        builder
            .Property(c => c.Label)
            .HasColumnName("label")
            .IsRequired()
            .HasMaxLength(50);
    }
}