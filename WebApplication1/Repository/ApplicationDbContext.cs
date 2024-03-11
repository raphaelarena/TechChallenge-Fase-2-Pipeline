using Microsoft.EntityFrameworkCore;
using WebApplication1.Entity;

namespace WebApplication1.Repository
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetValue<string>("ConnectionStrings:ConnectionString"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(u =>
            {
                u.ToTable("User");
                u.HasKey(u => u.Id);
                u.Property(u => u.Id).HasColumnType("uniqueidentifier");
                u.Property(u => u.Name).HasColumnType("NVARCHAR(200)");
                u.Property(u => u.UserName).HasColumnType("NVARCHAR(50)").IsRequired();
                u.Property(u => u.Password).HasColumnType("NVARCHAR(MAX)").IsRequired();
                u.Property(u => u.PermissionType).HasConversion<int>().IsRequired();
                u.Property(u => u.Status).HasColumnType("BIT").IsRequired();
            });
        }
    }
}
