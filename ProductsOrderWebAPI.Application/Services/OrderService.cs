using ProductsOrderWebAPI.Application.DTOs;
using ProductsOrderWebAPI.Application.Interfaces;
using ProductsOrderWebAPI.Application.Mappings;
using ProductsOrderWebAPI.Domain.Entities;
using ProductsOrderWebAPI.Domain.Exceptions;
using ProductsOrderWebAPI.Domain.Interfaces;

namespace ProductsOrderWebAPI.Application.Services
{
    public class OrderService(
        IOrderRepository orderRepository,
        IUnityOfWork unityOfWork
    ) : IOrderService
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IUnityOfWork _unityOfWork = unityOfWork;

        public async Task<OrderDto> AddOrder(CreateOrderDto dto)
        {
            if (dto.ProductsList.Count != 0)
            {
                var order = new Order
                {
                    ProductsList = dto.ProductsList,
                };

                await _orderRepository.AddOrderAsync(order);
                await _unityOfWork.CommitChangesAsync();

                var orderWithTotalValue = await FindById(order.Id);

                return orderWithTotalValue!;
            }

            throw new OrderWithNoProductsException();
        }

        public async Task<OrderDto?> FindById(int id)
        {
            var order = await _orderRepository.FindById(id);
            return order?.ToDto();
        }

        public async Task<OrderDto> UpdateOrder(UpdateOrderDto dto)
        {
            var order = await _orderRepository.FindByIdForUpdateAsync(dto.Id);

            if (order != null)
            {
                order.ProductsList.Clear();

                foreach (var product in dto.ProductsList)
                {
                    order.ProductsList.Add(product);
                }

                order.UpdatedAt = DateTime.Now;

                await _unityOfWork.CommitChangesAsync();

                return order.ToDto();
            }

            throw new OrderNotFoundException(dto.Id);
        }
        public async Task DeleteOrder(int id)
        {
            var order = await _orderRepository.FindById(id);

            if (order != null)
            {
                await _orderRepository.DeleteOrderAsync(order);
                await _unityOfWork.CommitChangesAsync();
                return;
            }

            throw new OrderNotFoundException(id);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByFilter(decimal totalPrice)
        {
            var orders = await _orderRepository.GetOrdersByFilterAsync(totalPrice);

            return orders.Select(o => o.ToDto());
        }
    }
}
