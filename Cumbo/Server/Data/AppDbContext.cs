using Cumbo.Server.Models;
using Cumbo.Server.Models.KupujemProdajemAd;
using Cumbo.Shared.DTOs.Advertisement;
using Microsoft.EntityFrameworkCore;

namespace Cumbo.Server.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<ProductModel> ProductModels { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Advertisment> Advertisments { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manufacturer>()
               .HasMany(m => m.ProductModels)
               .WithOne(pm => pm.Manufacturer)
               .OnDelete(DeleteBehavior.Restrict)
               .HasForeignKey(pm => pm.ManufacturerId);

            modelBuilder.Entity<ProductModel>()
                .HasMany(pm => pm.Products)
                .WithOne(p => p.ProductModel)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(p => p.ProductModelId);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Transactions)
                .WithOne(t => t.Customer)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(t => t.CustomerId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Transactions)
                .WithOne(t => t.Product)
                .HasForeignKey(t => t.ProductId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Transactions)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Phone>().ToTable("Phones");

            base.OnModelCreating(modelBuilder);
        }
    }
}
