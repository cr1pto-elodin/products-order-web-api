using ProductsOrderWebAPI.Domain.Entities;

namespace ProductsOrderWebAPI.Domain.Interfaces
{
    public interface IProductsRepository
    {
        Task<Product?> FindById(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Product product);
    }
}
