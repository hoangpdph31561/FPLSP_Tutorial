using FPLSP_Tutorial.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FPLSP_Tutorial.Infrastructure.Database.Configurations
{
    public class UserMajorEntityConfigurations : IEntityTypeConfiguration<UserMajorEntity>
    {
        public void Configure(EntityTypeBuilder<UserMajorEntity> builder)
        {
            builder.ToTable("UserMajor");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.HasOne(c => c.Major).WithMany(c => c.UserMajors).HasForeignKey(c => c.MajorId);
            builder.HasOne(c => c.User).WithMany(c => c.UserMajors).HasForeignKey(c => c.UserId);
        }
    }
}
