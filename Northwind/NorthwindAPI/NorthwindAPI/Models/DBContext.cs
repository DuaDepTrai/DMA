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
        public DbSet<Suppliers> Suppliers { get; set; }
        public DbSet<Shippers> Shippers { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<EmployeeTerritories> EmployeeTerritories { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeTerritories>()
                .HasKey(et => new { et.EmployeeID, et.TerritoryID });
            modelBuilder.Entity<OrderDetails>()
                .HasKey(et => new { et.OrderID, et.ProductID });
            // Cấu hình mối quan hệ tự tham chiếu cho Employees (đã có)
            //modelBuilder.Entity<Employees>()
            //    .HasOne(e => e.ReportsToNavigation)
            //    .WithMany()
            //    .HasForeignKey(e => e.ReportsTo)
            //    .IsRequired(false); // ReportsTo có thể null

            // Cấu hình mối quan hệ giữa Users và Employees
            //modelBuilder.Entity<Users>()
            //    .HasOne(u => u.Employees)
            //    .WithMany()
            //    .HasForeignKey(u => u.EmployeeID)
            //    .IsRequired(false); // EmployeeID có thể null
        }


    }
}
