using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Username);

        builder
            .Property(u => u.Username)
            .HasColumnName("username");

        builder
            .Property(u => u.Email)
            .IsRequired()
            .HasColumnName("email")
            .HasMaxLength(50);
    }
}