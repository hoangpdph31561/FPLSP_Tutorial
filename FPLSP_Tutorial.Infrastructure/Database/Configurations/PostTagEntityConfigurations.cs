using FPLSP_Tutorial.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FPLSP_Tutorial.Infrastructure.Database.Configurations
{
    public class PostTagEntityConfigurations : IEntityTypeConfiguration<PostTagEntity>
    {
        public void Configure(EntityTypeBuilder<PostTagEntity> builder)
        {
            builder.ToTable("PostTag");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.HasOne(c => c.Tag).WithMany(c => c.PostTags).HasForeignKey(c => c.TagId);
            builder.HasOne(c => c.Post).WithMany(c => c.PostTags).HasForeignKey(c => c.PostId);
        }
    }
}
