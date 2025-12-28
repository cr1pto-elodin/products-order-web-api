using System.ComponentModel.DataAnnotations;

namespace ProductsOrderWebAPI.Application.DTOs
{
    public class CreateProductDto(int idOrder, string name, decimal price)
    {
        
        [Range(1, int.MaxValue, ErrorMessage = "IdOrder must have a valid value")]
        public int IdOrder { get; set; } = idOrder;

        [Required(ErrorMessage = "Product must have a name")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Name must have from 3 to 200 chars")]
        public string Name { get; set; } = name;

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be positive")]
        public decimal Price { get; set; } = price;
    }
}
