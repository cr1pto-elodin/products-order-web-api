using System.ComponentModel.DataAnnotations;
using ProductsOrderWebAPI.Domain.Entities;

namespace ProductsOrderWebAPI.Application.DTOs
{
    public class CreateOrderDto( List<Product> productsList)
    {
        [Required(ErrorMessage = "Products list must have a value")]
        [MinLength(1, ErrorMessage = "Order must have at least one product")]
        public List<Product> ProductsList { get; set; } = productsList;
    }
}
