
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Storeify.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Store> Store { get; set; } = default!;
        public DbSet<Branch> Branches { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Customer> Customers { get; set; } = default!;
        public DbSet<Department> Departments { get; set; } = default!;
        public DbSet<Employee> Employees { get; set; } = default!;
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Review> Reviews { get; set; } = default!;
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<DeliveryCompany> DeliveryCompanies { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<InventoryProduct> InventoryProducts { get; set; }
        public DbSet<DiscountProduct> DiscountProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Review
            builder.Entity<Review>()
            .Property(r => r.Rating)
            .HasPrecision(3, 2);

            //InventoryProduct
            builder.Entity<InventoryProduct>()
            .HasKey(ip => new { ip.InventoryId, ip.ProductId });

            //DiscountProduct
            builder.Entity<DiscountProduct>()
            .HasKey(d => new { d.DiscountId, d.ProductId });

            base.OnModelCreating(builder);
        }

        public async Task RecalculateAllProductStockQuantitiesAsync()
        {
            await Database.ExecuteSqlRawAsync(@"
            UPDATE Products
            SET StockQuantity = (
                SELECT ISNULL(SUM(ip.Quantity), 0)
                FROM InventoryProducts ip
                WHERE ip.ProductId = Products.Id
            )
        ");
        }
    }
}
