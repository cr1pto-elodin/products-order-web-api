using ProductsOrderWebAPI.Infrastructure.Repositories;
using ProductsOrderWebAPI.Tests.IntegrationTests.Base;
using ProductsOrderWebAPI.Tests.Mocks;

namespace ProductsOrderWebAPI.Tests.IntegrationTests.Infrastructure.Repositories
{
    public class OrderRepositoryTests : BaseTest
    {
        private readonly OrderRepository _repository;

        public OrderRepositoryTests()
        {
            _repository = new OrderRepository(_context);
        }

        [Fact]
        public async Task AddOrderAsync()
        {
            var order = ObjectFaker.GenerateOrder();
            order.ProductsList = ObjectFaker.GenerateProductList(3);

            await _repository.AddOrderAsync(order);
            await _context.SaveChangesAsync();

            var resultOrder = await _context.Order.FindAsync(order.Id);

            Assert.NotNull(resultOrder);
            Assert.Equal(order.Id, resultOrder.Id);
            Assert.NotEmpty(resultOrder.ProductsList);
            Assert.Equal(3, order.ProductsList.Count);
            Assert.Contains(resultOrder.ProductsList, p => p.Name != null);
        }

        [Fact]
        public async Task FindById()
        {
            var order = ObjectFaker.GenerateOrder();
            order.ProductsList = ObjectFaker.GenerateProductList(3);
            
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            var result = await _repository.FindById(order.Id);

            Assert.NotNull(result);
            Assert.Equal(order.Id, result.Id);
            Assert.Equal(3, result.ProductsList.Count);
        }

        [Fact]
        public async Task UpdateOrderAsync()
        {
            var order = ObjectFaker.GenerateOrder();
            await _context.Order.AddAsync(order);
            await _context.SaveChangesAsync();

            var product = ObjectFaker.GenerateProduct();
            order.ProductsList.Add(product);

            await _repository.UpdateOrderAsync(order);
            await _context.SaveChangesAsync();

            var updatedOrder = await _context.Order.FindAsync(order.Id);

            Assert.NotNull(updatedOrder);
            Assert.Equal(product, updatedOrder.ProductsList[0]);
        }
    }
}
