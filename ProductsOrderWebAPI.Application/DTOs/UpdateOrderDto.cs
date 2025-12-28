using ProductsOrderWebAPI.Domain.Entities;

namespace ProductsOrderWebAPI.Application.DTOs
{
    public class UpdateOrderDto(int id, List<Product> products)
    {
        public int Id { get; set; } = id;
        public List<Product> ProductsList { get; set; } = products;
    }
}
