namespace ProductsOrderWebAPI.Application.DTOs
{
    public class CreateProductDto(int idOrder, decimal price)
    {
        public required int IdOrder { get; set; } = idOrder;
        public required decimal Price { get; set; } = price;
    }
}
