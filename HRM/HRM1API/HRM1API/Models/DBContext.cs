using Microsoft.EntityFrameworkCore;

namespace HRM1API.Models
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<Sex> Sex { get; set; }

    }
}
