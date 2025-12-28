namespace ProductsOrderWebAPI.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; } = 0;
        public required List<Product> ProductsList { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
