using ProductsOrderWebAPI.Application.DTOs;

namespace ProductsOrderWebAPI.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> AddOrder(CreateOrderDto dto);
        Task<OrderDto?> FindById(int id);
        Task<OrderDto> UpdateOrder(UpdateOrderDto dto);
        Task DeleteOrder(int id);
    }
}
