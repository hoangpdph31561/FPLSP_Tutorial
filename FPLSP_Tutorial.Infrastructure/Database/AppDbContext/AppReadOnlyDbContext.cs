using FPLSPTutorial.Domain.Entities;
using FPLSPTutorial.Infrastructure.Database.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Tutorial.Infrastructure.Database.AppDbContext
{
    public class AppReadOnlyDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExampleReadOnlyDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=66.42.55.38;Initial Catalog=FPLSP_Tutorial;User ID=test;Password=E=lPJeY>-g/9QxzE;MultipleActiveResultSets=true;TrustServerCertificate=True;");
            }
        }

        public DbSet<LevelEntity> Levels { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<PostEntity> Posts { get; set; }
    }
}
