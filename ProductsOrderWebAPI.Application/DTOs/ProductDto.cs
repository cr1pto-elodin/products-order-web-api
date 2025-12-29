namespace ProductsOrderWebAPI.Application.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}