namespace ProductsOrderWebAPI.Application.DTOs
{
    public class CreateProductDto(int idOrder, string name, decimal price)
    {
        public required int IdOrder { get; set; } = idOrder;
        public required string Name { get; set; } = name;
        public required decimal Price { get; set; } = price;
    }
}
