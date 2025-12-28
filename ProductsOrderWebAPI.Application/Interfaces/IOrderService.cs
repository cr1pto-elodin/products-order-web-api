using ProductsOrderWebAPI.Application.DTOs;
using ProductsOrderWebAPI.Domain.Entities;

namespace ProductsOrderWebAPI.Application.Interfaces
{
    public interface IOrderService
    {
        Task<int> AddOrder(CreateOrderDto dto);
        Task<Order?> FindById(int id);
        Task<int> UpdateOrder(UpdateOrderDto dto);
    }
}
