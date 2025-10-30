using Microsoft.EntityFrameworkCore;

namespace OrderApp.Models
{
    public partial class OrderDBContext : DbContext
    {
        public OrderDBContext() { }
        public OrderDBContext(DbContextOptions<OrderDBContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<PurchaseItem> PurchaseItems { get; set; }

        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

        public virtual DbSet<SKU> Skus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships if needed (e.g., one-to-many between PurchaseOrder and PurchaseOrderItem)
            modelBuilder.Entity<PurchaseOrder>()
                .HasMany(po => po.Items)
                .WithOne(poi => poi.PurchaseOrder)
                .HasForeignKey(poi => poi.PurchaseOrderId);
        }
    }
}
