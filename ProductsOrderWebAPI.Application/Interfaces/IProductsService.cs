using ProductsOrderWebAPI.Application.DTOs;
using ProductsOrderWebAPI.Domain.Entities;

namespace ProductsOrderWebAPI.Application.Interfaces
{
    public interface IProductsService
    {
        Task<Product?> FindById(int id);
        Task<Product> AddProduct(CreateProductDto dto);
        Task<Product> UpdateProduct(UpdateProductDto dto);
    }
}
