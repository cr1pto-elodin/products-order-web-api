using ProductsOrderWebAPI.Application.DTOs;
using ProductsOrderWebAPI.Domain.Entities;

namespace ProductsOrderWebAPI.Application.Interfaces
{
    public interface IProductsService
    {
        Task<Product?> FindById(int id);
        Task<int> AddProduct(CreateProductDto dto);
        Task<int> UpdateProduct(UpdateProductDto dto);
    }
}
