using ProductsOrderWebAPI.Application.DTOs;
using ProductsOrderWebAPI.Application.Interfaces;
using ProductsOrderWebAPI.Domain.Entities;
using ProductsOrderWebAPI.Domain.Exceptions;
using ProductsOrderWebAPI.Domain.Interfaces;

namespace ProductsOrderWebAPI.Application.Services
{
    public class ProductsService(
        IOrderService orderService,
        IProductsRepository productsRepository,
        IUnityOfWork unityOfWork
    ) : IProductsService
    {
        private readonly IProductsRepository _productsRepository = productsRepository;
        private readonly IOrderService _orderService = orderService;
        private readonly IUnityOfWork _unityOfWork = unityOfWork;

        public async Task<Product?> FindById(int id)
        {
            return await _productsRepository.FindById(id);
        }

        public async Task<Product> AddProduct(CreateProductDto dto)
        {
            var order = await _orderService.FindById(dto.IdOrder);

            if (order != null)
            {
                var product = new Product
                {
                    IdOrder = dto.IdOrder,
                    Name = dto.Name,
                    Price = dto.Price
                };

                await _productsRepository.AddProductAsync(product);

                await _unityOfWork.CommitChangesAsync();

                return product;
            }

            throw new OrderNotFoundException(dto.IdOrder);
        }

        public async Task<Product> UpdateProduct(UpdateProductDto dto)
        {
            var product = await _productsRepository.FindById(dto.Id);

            if (product != null)
            {
                product.Price = dto.Price;
                product.UpdatedAt = DateTime.Now;

                await _unityOfWork.CommitChangesAsync();

                return product;
            }

            throw new ProductNotFoundException(dto.Id);
        }
    }
}
