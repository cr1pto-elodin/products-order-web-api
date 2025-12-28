using ProductsOrderWebAPI.Application.DTOs;
using ProductsOrderWebAPI.Domain.Entities;

namespace ProductsOrderWebAPI.Application.Interfaces
{
    public interface IOrderService
    {
        Task<Order> AddOrder(CreateOrderDto dto);
        Task<Order?> FindById(int id);
        Task<Order> UpdateOrder(UpdateOrderDto dto);
    }
}
