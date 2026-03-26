using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TaskManagementApi.Models;

namespace TaskManagementApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks { get; set; }
    }

    // EF Core aracının (Migration) veritabanı haritasını çıkarırken
    // bağlantıyı şaşırmadan bulabilmesi için bu Fabrika (Factory) sınıfını ekliyoruz.
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite("Data Source=tasks.db");
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
