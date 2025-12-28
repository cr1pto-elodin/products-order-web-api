using Microsoft.EntityFrameworkCore;
using ProductsOrderWebAPI.Domain.Entities;
using ProductsOrderWebAPI.Domain.Interfaces;
using ProductsOrderWebAPI.Infrastructure.Data;

namespace ProductsOrderWebAPI.Infrastructure.Repositories
{
    public class OrderRepository(
        AppDbContext appDbContext
    ) : IOrderRepository
    {
        private readonly AppDbContext _context = appDbContext;
        public async Task AddOrderAsync(Order order)
        {
            await _context.AddAsync(order);
        }

        public async Task<Order?> FindById(int id)
        {
            return await _context.Order
                .Include(order => order.ProductsList)
                .FirstOrDefaultAsync(order => order.Id == id);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Order.Update(order);
        }
    }
}
