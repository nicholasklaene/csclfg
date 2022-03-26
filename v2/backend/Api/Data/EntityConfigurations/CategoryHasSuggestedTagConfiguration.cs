using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.EntityConfigurations;

public class CategoryHasSuggestedTagConfiguration : IEntityTypeConfiguration<CategoryHasSuggestedTag>
{
    public void Configure(EntityTypeBuilder<CategoryHasSuggestedTag> builder)
    {
        builder.ToTable("category_has_suggested_tag");
        
        builder.HasKey(ct => new { ct.CategoryId, ct.TagLabel });

        builder
            .Property(ct => ct.CategoryId)
            .HasColumnName("category_id");
        builder
            .Property(ct => ct.TagLabel)
            .HasColumnName("tag_label");
    }
}