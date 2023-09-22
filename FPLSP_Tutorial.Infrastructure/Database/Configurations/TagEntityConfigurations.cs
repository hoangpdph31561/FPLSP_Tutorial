using FPLSP_Tutorial.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FPLSP_Tutorial.Infrastructure.Database.Configurations
{
    public class TagEntityConfigurations : IEntityTypeConfiguration<TagEntity>
    {
        public void Configure(EntityTypeBuilder<TagEntity> builder)
        {
            builder.ToTable("Tag");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.MajorId).IsRequired(false);

            builder.HasOne(c => c.Major).WithMany(c => c.Tags).HasForeignKey(c => c.MajorId);
        }
    }
}
