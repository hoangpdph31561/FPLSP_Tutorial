using FPLSP_Tutorial.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FPLSP_Tutorial.Infrastructure.Database.AppDbContext
{
    public class AppReadOnlyDbContext : DbContext
    {
        public AppReadOnlyDbContext()
        {
        }

        public AppReadOnlyDbContext(DbContextOptions<AppReadOnlyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppReadOnlyDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source=66.42.55.38;" +
                    "Initial Catalog=FPLSP_Tutorial;" +
                    "User ID=test;" +
                    "Password=E=lPJeY>-g/9QxzE;" +
                    "MultipleActiveResultSets=true;" +
                    "TrustServerCertificate=True;");
            }
        }

        public DbSet<MajorEntity> MajorEntities { get; set; }
        public DbSet<UserEntity> UserEntities { get; set; }
        public DbSet<UserMajorEntity> UserMajorEntities { get; set; }
        public DbSet<PostEntity> PostEntities { get; set; }
        public DbSet<TagEntity> TagEntities { get; set; }
        public DbSet<PostTagEntity> PostTagEntities { get; set; }
        public DbSet<MajorRequestEntity> MajorRequestEntities { get; set; }
    }
}
