using ProductsOrderWebAPI.Domain.Entities;

namespace ProductsOrderWebAPI.Application.DTOs
{
    public class UpdateOrderDto(List<Product> products)
    {
        public int Id { get; set; }
        public List<Product> ProductsList { get; set; } = products;
    }
}
