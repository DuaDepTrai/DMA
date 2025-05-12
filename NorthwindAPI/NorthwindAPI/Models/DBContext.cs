using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Controllers;

namespace NorthwindAPI.Models
{
    public class MyDBContext: DbContext
    {
        public MyDBContext(DbContextOptions options) : base (options) { }

        public DbSet<Region> Region { get; set; }
        //public DbSet<CustomersController> Customers { get; set; }
        public DbSet<Customers> Customers { get; set; }

        public DbSet<Categories> Categories { get; set; }
        public DbSet<Territories> Territories { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
