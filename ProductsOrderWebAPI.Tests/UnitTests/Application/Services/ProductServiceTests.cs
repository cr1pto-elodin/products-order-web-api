using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Moq;
using ProductsOrderWebAPI.Application.DTOs;
using ProductsOrderWebAPI.Application.Interfaces;
using ProductsOrderWebAPI.Application.Mappings;
using ProductsOrderWebAPI.Application.Services;
using ProductsOrderWebAPI.Domain.Entities;
using ProductsOrderWebAPI.Domain.Exceptions;
using ProductsOrderWebAPI.Domain.Interfaces;
using ProductsOrderWebAPI.Tests.Mocks;

namespace ProductsOrderWebAPI.Tests.UnitTests.Application.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductsRepository> _repositoryMock;
        private readonly Mock<IOrderService> _orderServiceMock;
        private readonly Mock<IUnityOfWork> _unityOfWorkMock;
        private readonly ProductsService _service;

        public ProductServiceTests()
        {
            _repositoryMock = new Mock<IProductsRepository>();
            _orderServiceMock = new Mock<IOrderService>();
            _unityOfWorkMock = new Mock<IUnityOfWork>();

            _service = new ProductsService(
                _orderServiceMock.Object,
                _repositoryMock.Object,
                _unityOfWorkMock.Object
            );
        }

        [Fact]
        public async Task CreateProduct_ShouldCallRepository()
        {
            var createProductDto = ObjectFaker.GenerateCreateProductDTO();
            var order = ObjectFaker.GenerateOrder();

            _orderServiceMock.Setup(s => s.FindById(createProductDto.IdOrder))
                             .ReturnsAsync((OrderDto?)order.ToDto());

            var result = await _service.AddProduct(createProductDto);

            Assert.NotNull(result);
            _repositoryMock.Verify(r => r.AddProductAsync(It.IsAny<Product>()), Times.Once);
            _orderServiceMock.Verify(s => s.FindById(It.IsAny<int>()), Times.Once);
            _unityOfWorkMock.Verify(r => r.CommitChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateProduct_InvalidIdOrder_ShouldThrowException()
        {
            var dto = ObjectFaker.GenerateCreateProductDTO();

            _orderServiceMock.Setup(s => s.FindById(dto.IdOrder))
                             .ReturnsAsync((OrderDto?)null);

            var action = async () => await _service.AddProduct(dto);

            await action.Should().ThrowAsync<OrderNotFoundException>()
                .WithMessage($"Order with id: {dto.IdOrder} was not found");

            _repositoryMock.Verify(r => r.AddProductAsync(It.IsAny<Product>()), Times.Never);
            _unityOfWorkMock.Verify(u => u.CommitChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task UpdateProduct_IdInvalid_ShouldThrowException()
        {
            var updateProductDto = ObjectFaker.GenerateUpdateProductDTO;

            _repositoryMock.Setup(r => r.FindById(updateProductDto.Id))
                           .ReturnsAsync((Product?)null);

            var action = async () => await _service.UpdateProduct(updateProductDto);

            await action.Should().ThrowAsync<ProductNotFoundException>()
                .WithMessage($"Product with id: {updateProductDto.Id} was not found");

            _unityOfWorkMock.Verify(r => r.CommitChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task DeleteOrder_ShouldCallRepositoryAndCommit()
        {
            var product = ObjectFaker.GenerateProduct();
            product.Id = 1;

            _repositoryMock.Setup(r => r.FindById(product.Id)).ReturnsAsync(product);

            await _service.DeleteProduct(product.Id);

            _repositoryMock.Verify(r => r.DeleteProductAsync(product), Times.Once);
            _unityOfWorkMock.Verify(u => u.CommitChangesAsync(), Times.Once);
        }
    }
}
