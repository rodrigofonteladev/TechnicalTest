using TechnicalTest.Models;
using Microsoft.EntityFrameworkCore;

namespace TechnicalTest.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Ministry> Ministries { get; set; }
    }
}
