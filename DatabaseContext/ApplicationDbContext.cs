using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SupplyChain.Models;

namespace SupplyChain.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt):base(opt)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<RestockRequest> RestockRequest { get; set; }
        public DbSet<InventoryAuditLog> InventoryAuditLog { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderItem> OrderItem{ get; set; }
        public DbSet<Product> Product { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the CreatedAt column to have a default value
            modelBuilder.Entity<Customer>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETDATE()"); // For SQL Server


            //      modelBuilder.Entity<InventoryAuditLog>()
            //.HasKey(l => l.LogID);
            // Explicit key configuration

            // Configure relationship with Product
            //modelBuilder.Entity<InventoryAuditLog>()
            //    .HasOne(l => l.Product)
            //    .WithMany()
            //    .HasForeignKey(l => l.ProductID);

            // Your other configurations...
            modelBuilder.Entity<RestockRequest>()
                .HasKey(r => r.RequestID);

            // Explicitly configure Identity table keys if needed
            //modelBuilder.Entity<IdentityUserLogin<Guid>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
            //modelBuilder.Entity<IdentityUserRole<Guid>>().HasKey(r => new { r.UserId, r.RoleId });
            //modelBuilder.Entity<IdentityUserToken<Guid>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
            //modelBuilder.Entity<IdentityUserClaim<Guid>>().HasKey(uc => uc.Id);
            //modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasKey(rc => rc.Id);

        }
    }
}
