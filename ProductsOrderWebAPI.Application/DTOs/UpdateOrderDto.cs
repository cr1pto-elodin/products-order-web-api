using System.ComponentModel.DataAnnotations;
using ProductsOrderWebAPI.Domain.Entities;

namespace ProductsOrderWebAPI.Application.DTOs
{
    public class UpdateOrderDto(int id, List<Product> products)
    {
        [Range(1, int.MaxValue, ErrorMessage = "Id must have a valid value")]
        public int Id { get; set; } = id;
        
        [Required(ErrorMessage = "Products list must have a value")]
        [MinLength(1, ErrorMessage = "Order must have at least one product")]
        public List<Product> ProductsList { get; set; } = products;
    }
}
