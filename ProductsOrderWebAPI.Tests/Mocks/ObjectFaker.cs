using Bogus;
using ProductsOrderWebAPI.Application.DTOs;
using ProductsOrderWebAPI.Domain.Entities;

namespace ProductsOrderWebAPI.Tests.Mocks
{
    public static class ObjectFaker
    {
        public static Faker<Product> ProductFaker =>
            new Faker<Product>("pt_BR")
                .RuleFor(p => p.Id, f => 0)
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Price, f => f.Finance.Amount(10, 5000))
                .RuleFor(p => p.CreatedAt, f => DateTime.Now)
                .RuleFor(p => p.UpdatedAt, f => DateTime.Now);

        public static Faker<Order> OrderFaker =>
            new Faker<Order>("pt_BR")
                .RuleFor(o => o.Id, f => 0)
                .RuleFor(o => o.TotalPrice, 0)
                .RuleFor(o => o.CreatedAt, f => DateTime.Now)
                .RuleFor(p => p.UpdatedAt, f => DateTime.Now);

        public static Product GenerateProduct() => ProductFaker.Generate();
        public static List<Product> GenerateProductList(int count) => ProductFaker.Generate(count);

        public static Order GenerateOrder() => OrderFaker.Generate();
        public static CreateOrderDto GenerateCreateOrderDTO(List<Product> products)
        {
            var dto = new Faker<CreateOrderDto>("pt_BR")
                .CustomInstantiator(f => new CreateOrderDto(
                    products
                ));
    
            return dto;
        }

        public static UpdateOrderDto GenerateUpdateOrderDTO(int id, List<Product> products)
        {
            var dto = new Faker<UpdateOrderDto>("pt_BR")
                .CustomInstantiator(f => new UpdateOrderDto(
                    id,
                    products
                ));
    
            return dto;
        }
    }
}
