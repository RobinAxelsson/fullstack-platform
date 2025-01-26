using Microsoft.EntityFrameworkCore;
using TenStar.UserContext.Entities;

namespace TenStar.UserContext.DataLayer
{
    internal class UserContextDbContext : DbContext
    {
        public UserContextDbContext() { }

        public UserContextDbContext(DbContextOptions<UserContextDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.DbId);
                entity.Property(u => u.Name).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(255);
                entity.Property(u => u.Password).IsRequired().HasMaxLength(255);
                entity.Property(u => u.Username).IsRequired().HasMaxLength(100);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (Environment.GetEnvironmentVariable("USE_INMEMORY_DB")?.ToUpper() == "TRUE")
            {
                optionsBuilder.UseInMemoryDatabase("TenStarDb");
                return;
            }

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=TenStar.Db;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;");
            }
        }
    }
}
