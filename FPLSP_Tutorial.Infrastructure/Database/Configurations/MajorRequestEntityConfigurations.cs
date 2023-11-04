using FPLSP_Tutorial.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FPLSP_Tutorial.Infrastructure.Database.Configurations
{
    public class MajorRequestEntityConfigurations : IEntityTypeConfiguration<MajorRequestEntity>
    {
        public void Configure(EntityTypeBuilder<MajorRequestEntity> builder)
        {
            builder.ToTable("MajorRequest");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.HasOne(c => c.Major).WithMany(c => c.MajorRequests).HasForeignKey(c => c.MajorId);
        }
    }
}
