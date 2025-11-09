using GeekGarden.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GeekGarden.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<EmailHistory> EmailHistories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<KPI> KPIs { get; set; }
    }
}
