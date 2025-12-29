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
                .AsNoTracking()
                .Include(order => order.ProductsList)
                .FirstOrDefaultAsync(order => order.Id == id);
        }

        public async Task<Order?> FindByIdForUpdateAsync(int id)
        {
            return await _context.Order
                .Include(o => o.ProductsList)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Order.Update(order);
        }

        public async Task DeleteOrderAsync(Order order)
        {

            _context.Order.Remove(order);
        }

        public async Task<IEnumerable<Order>> GetOrdersByFilterAsync(decimal totalPrice)
        {
            var orders = await _context.Order
            .FromSqlRaw("EXEC SP_GetOrdersByTotalPrice @TotalPrice = {0}", totalPrice)
            .AsNoTracking()
            .ToListAsync();

            return orders;
        }
    }
}
