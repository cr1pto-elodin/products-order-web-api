using ProductsOrderWebAPI.Application.DTOs;
using ProductsOrderWebAPI.Domain.Entities;

namespace ProductsOrderWebAPI.Application.Interfaces
{
    public interface IProductsService
    {
        Task<ProductDto?> FindById(int id);
        Task<ProductDto> AddProduct(CreateProductDto dto);
        Task<ProductDto> UpdateProduct(UpdateProductDto dto);
    }
}
