using Microsoft.EntityFrameworkCore;
using SupplyChain.Models;

namespace SupplyChain.DatabaseContext
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt):base(opt)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<RestockRequest> RestockRequest { get; set; }
        public DbSet<InventoryAuditLog> InventoryAuditLog { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderItem> OrderItem{ get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the CreatedAt column to have a default value
            modelBuilder.Entity<Customer>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETDATE()"); // For SQL Server


            modelBuilder.Entity<InventoryAuditLog>()
      .HasKey(l => l.LogID); // Explicit key configuration

            // Configure relationship with Product
            modelBuilder.Entity<InventoryAuditLog>()
                .HasOne(l => l.Product)
                .WithMany()
                .HasForeignKey(l => l.ProductID);

            // Your other configurations...
            modelBuilder.Entity<RestockRequest>()
                .HasKey(r => r.RequestID);

        }
    }
}
