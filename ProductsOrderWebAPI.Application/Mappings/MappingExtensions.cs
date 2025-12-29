using ProductsOrderWebAPI.Application.DTOs;
using ProductsOrderWebAPI.Domain.Entities;

namespace ProductsOrderWebAPI.Application.Mappings
{
    public static class MappingExtensions
    {
        public static OrderDto ToDto(this Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                TotalPrice = order.TotalPrice,
                ProductsList = order.ProductsList?.Select(p => p.ToDto()).ToList() 
                       ?? [],
                CreatedAt = order.CreatedAt,
                UpdatedAt = order.UpdatedAt,
            };
        }

        public static ProductDto ToDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}