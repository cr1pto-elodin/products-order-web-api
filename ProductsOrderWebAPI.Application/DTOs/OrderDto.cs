namespace ProductsOrderWebAPI.Application.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; } = 0;
        public required List<ProductDto> ProductsList { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
