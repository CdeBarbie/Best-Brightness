using FinalBestBrightnessStore.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalBestBrightnessStore
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<SalesPerson> SalesPersons { get; set; }
        public DbSet<StockMananger> StockManangers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SalesTracking> SaleTrack { get; set; }
        public DbSet<StockTracking> StockTrack { get; set; }
        public DbSet<Finance> Finances { get; set; }
        public DbSet<SalesTrackingProduct> SalesTrackingProducts { get; set; }  // Add this line
        public DbSet<CustomerOrder> CustomerOrders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Add any specific configurations here
            // For example, configure relationships if needed
            modelBuilder.Entity<SalesTracking>()
                .HasMany(st => st.ProductsSold)
                .WithOne(stp => stp.SalesTracking)
                .HasForeignKey(stp => stp.salesTrackId);

        }

    }
}
