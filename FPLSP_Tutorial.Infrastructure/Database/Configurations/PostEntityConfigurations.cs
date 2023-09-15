using FPLSPTutorial.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace FPLSP_Tutorial.Infrastructure.Database.Configurations
{
    public class PostEntityConfigurations : IEntityTypeConfiguration<PostEntity>
    {
        public void Configure(EntityTypeBuilder<PostEntity> builder)
        {
            builder.ToTable("Post");
            builder.HasKey(x => x.Id);

            builder.HasOne(c => c.CategoryEntity).WithMany(c => c.PostEntities).HasForeignKey(c => c.CategoryId);
            builder.HasOne(c => c.LevelEntity).WithMany(c => c.PostEntities).HasForeignKey(c => c.LevelId);
        }
    }
}
