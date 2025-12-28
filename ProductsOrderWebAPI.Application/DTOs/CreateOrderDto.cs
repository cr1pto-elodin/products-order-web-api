using ProductsOrderWebAPI.Domain.Entities;

namespace ProductsOrderWebAPI.Application.DTOs
{
    public class CreateOrderDto( List<Product> productsList)
    {
        public List<Product> ProductsList { get; set; } = productsList;
    }
}
