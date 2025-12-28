using ProductsOrderWebAPI.Domain.Entities;

namespace ProductsOrderWebAPI.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> FindById(int id);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
    }
}
