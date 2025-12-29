using ProductsOrderWebAPI.Infrastructure.Repositories;
using ProductsOrderWebAPI.Tests.IntegrationTests.Base;
using ProductsOrderWebAPI.Tests.Mocks;

namespace ProductsOrderWebAPI.Tests.IntegrationTests.Infrastructure.Repositories
{
    public class ProductRepositoryTests : BaseTest
    {
        private readonly ProductsRepository _productsRepository;
        private readonly int _idOrder;

        public ProductRepositoryTests()
        {
            _productsRepository = new ProductsRepository(_context);

            var order = ObjectFaker.GenerateOrder();
            order.ProductsList = ObjectFaker.GenerateProductList(1);

            _context.Order.Add(order);
            _context.SaveChanges();

            _idOrder = order.Id;
        }

        [Fact]
        public async Task AddProductAsync()
        {
            var product = ObjectFaker.GenerateProduct();
            product.IdOrder = _idOrder;

            await _productsRepository.AddProductAsync(product);
            await _context.SaveChangesAsync();

            var result = await _productsRepository.FindById(product.Id);

            Assert.NotNull(result);
            Assert.Equal(_idOrder, result.IdOrder);
        }

        [Fact]
        public async Task FindById()
        {
            var product = ObjectFaker.GenerateProduct();
            product.IdOrder = _idOrder;

            await _productsRepository.AddProductAsync(product);
            await _context.SaveChangesAsync();

            var result = await _productsRepository.FindById(product.Id);

            Assert.NotNull(result);
            Assert.Equal(product.Id, result.Id);
        }

        [Fact]
        public async Task UpdateOrderAsync()
        {
            var product = ObjectFaker.GenerateProduct();
            product.IdOrder = _idOrder;

            await _productsRepository.AddProductAsync(product);
            await _context.SaveChangesAsync();

            var newPrice = 100.00m;
            product.Price = newPrice;

            await _productsRepository.UpdateProductAsync(product);
            await _context.SaveChangesAsync();

            var updatedProduct = await _context.Products.FindAsync(product.Id);

            Assert.NotNull(updatedProduct);
            Assert.Equal(newPrice, updatedProduct.Price);
        }

        [Fact]
        public async Task DeleteOrder_ShouldRemoveFromDatabase()
        {
            var product = ObjectFaker.GenerateProduct();
            product.IdOrder = _idOrder;
            
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            await _productsRepository.DeleteProductAsync(product);
            await _context.SaveChangesAsync();

            var deletedProduct = await _context.Products.FindAsync(product.Id);
            Assert.Null(deletedProduct);
        }
    }
}
