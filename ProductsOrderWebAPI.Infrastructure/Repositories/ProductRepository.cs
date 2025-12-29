using Microsoft.EntityFrameworkCore;
using ProductsOrderWebAPI.Domain.Entities;
using ProductsOrderWebAPI.Domain.Interfaces;
using ProductsOrderWebAPI.Infrastructure.Data;

namespace ProductsOrderWebAPI.Infrastructure.Repositories
{
    public class ProductsRepository(
        AppDbContext context
    ) : IProductsRepository
    {
        private readonly AppDbContext _context = context;

        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task<Product?> FindById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
        }

        public async Task DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);
        }
    }
}
