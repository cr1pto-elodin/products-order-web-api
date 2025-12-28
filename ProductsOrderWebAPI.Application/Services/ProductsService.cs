using ProductsOrderWebAPI.Application.DTOs;
using ProductsOrderWebAPI.Application.Interfaces;
using ProductsOrderWebAPI.Domain.Entities;
using ProductsOrderWebAPI.Domain.Exceptions;
using ProductsOrderWebAPI.Domain.Interfaces;

namespace ProductsOrderWebAPI.Application.Services
{
    public class ProductsService(
        IProductsRepository productsRepository,
        IUnityOfWork unityOfWork
    ) : IProductsService {
        private readonly IProductsRepository _productsRepository = productsRepository;
        private readonly IUnityOfWork _unityOfWork = unityOfWork;

        public async Task<Product?> FindById(int id)
        {
            return await this._productsRepository.FindById(id);
        }

        public async Task<int> AddProduct(CreateProductDto dto)
        {
            var product = new Product {
                IdOrder = dto.IdOrder,
                Name = dto.Name,
                Price = dto.Price
            };

            await this._productsRepository.AddProductAsync(product);

            return await _unityOfWork.CommitChangesAsync();
        }

        public async Task<int> UpdateProduct(UpdateProductDto dto)
        {
            var product = await _productsRepository.FindById(dto.Id);

            if (product != null)
            {
                product.Price = dto.Price;
                product.UpdatedAt = DateTime.Now;

                return await _unityOfWork.CommitChangesAsync();
            }

            throw new ProductNotFoundException(dto.Id);
        }
    }
}
