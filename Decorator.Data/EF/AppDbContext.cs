using Decorator.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Decorator.Data.EF
{
    public class AppDbContext : DbContext
    {
        public DbSet<Products> Products { get; set; }
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Price).IsRequired();
            });

            modelBuilder.Entity<Products>().HasData(
                new Products() { ProductId = 1, Name = "shoes", Price = 10 },
                new Products() { ProductId = 2, Name = "clothes", Price = 10 },
                new Products() { ProductId = 3, Name = "computer", Price = 10 },
                new Products() { ProductId = 4, Name = "phone", Price = 10 }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
