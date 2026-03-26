using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using CryptoTrackerApi.Models;

namespace CryptoTrackerApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }

    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite("Data Source=crypto.db");
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
