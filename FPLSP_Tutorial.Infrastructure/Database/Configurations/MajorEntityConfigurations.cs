using FPLSP_Tutorial.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FPLSP_Tutorial.Infrastructure.Database.Configurations
{
    public class MajorEntityConfigurations : IEntityTypeConfiguration<MajorEntity>
    {
        public void Configure(EntityTypeBuilder<MajorEntity> builder)
        {
            builder.ToTable("Major");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
        }
    }
}
