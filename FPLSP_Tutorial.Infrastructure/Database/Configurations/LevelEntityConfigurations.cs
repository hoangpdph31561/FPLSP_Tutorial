using FPLSPTutorial.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Infrastructure.Database.Configurations
{
    public class LevelEntityConfigurations : IEntityTypeConfiguration<LevelEntity>
    {
        public void Configure(EntityTypeBuilder<LevelEntity> builder)
        {
            builder.ToTable("Level");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
