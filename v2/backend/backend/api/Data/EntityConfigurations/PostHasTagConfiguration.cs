using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.EntityConfigurations;

public class PostHasTagConfiguration : IEntityTypeConfiguration<PostHasTag>
{
    public void Configure(EntityTypeBuilder<PostHasTag> builder)
    {
        builder.ToTable("post_has_tag");
        
        builder.HasKey(pt => new { pt.PostId, pt.TagLabel });

        builder
            .Property(pt => pt.PostId)
            .HasColumnName("post_id");
        builder
            .Property(ct => ct.TagLabel)
            .HasColumnName("tag_label");
    }
}
