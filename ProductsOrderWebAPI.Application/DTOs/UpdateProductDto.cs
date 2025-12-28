using System.ComponentModel.DataAnnotations;

namespace ProductsOrderWebAPI.Application.DTOs
{
    public class UpdateProductDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Id must have a valid value")]
        public int Id { get; set; }
        
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be positive")]
        public decimal Price { get; set; }
    }
}
