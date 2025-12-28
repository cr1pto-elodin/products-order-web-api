using Microsoft.EntityFrameworkCore;
using ProductsOrderWebAPI.Domain.Entities;
using ProductsOrderWebAPI.Infrastructure.Mappings;

namespace ProductsOrderWebAPI.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Order { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
        }
    }
}
