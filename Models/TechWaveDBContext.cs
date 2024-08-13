using Microsoft.EntityFrameworkCore;
using TechWave.Models.DomainModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TechWave.Models
{
    public class TechWaveDBContext : IdentityDbContext<User>
    {
        public TechWaveDBContext(DbContextOptions<TechWaveDBContext> options)
            : base(options)
        {
        }

        // DbSets for each model
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurations for composite keys, relationships, etc.
            modelBuilder.Entity<Cart>()
                .HasKey(c => new { c.UserID, c.ProductID });

            // Optional: Configure decimal precision for Price, TotalAmount, and TotalTax
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalTax)
                .HasColumnType("decimal(18,2)");
        }
        }
    }
