using FluentAssertions;
using Moq;
using ProductsOrderWebAPI.Application.Services;
using ProductsOrderWebAPI.Domain.Entities;
using ProductsOrderWebAPI.Domain.Exceptions;
using ProductsOrderWebAPI.Domain.Interfaces;
using ProductsOrderWebAPI.Tests.Mocks;

namespace ProductsOrderWebAPI.Tests.UnitTests.Application.Services
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _repositoryMock;
        private readonly Mock<IUnityOfWork> _unityOfWorkMock;
        private readonly OrderService _service;

        public OrderServiceTests()
        {
            _repositoryMock = new Mock<IOrderRepository>();
            _unityOfWorkMock = new Mock<IUnityOfWork>();

            _service = new OrderService(
                _repositoryMock.Object,
                _unityOfWorkMock.Object
            );
        }

        [Fact]
        public async Task CreateOrder_ShouldCallRepository()
        {
            var products = ObjectFaker.GenerateProductList(2);
            var createOrderDto = ObjectFaker.GenerateCreateOrderDTO(products);

            var result = await _service.AddOrder(createOrderDto);

            _repositoryMock.Verify(r => r.AddOrderAsync(It.IsAny<Order>()), Times.Once);
            _unityOfWorkMock.Verify(r => r.CommitChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateOrder_EmptyProducts_ShouldThrowException()
        {
            var products = ObjectFaker.GenerateProductList(0);
            var createOrderDto = ObjectFaker.GenerateCreateOrderDTO(products);

            var action = async () => await _service.AddOrder(createOrderDto);

            await action.Should().ThrowAsync<OrderWithNoProductsException>()
                .WithMessage("Order needs to have at least one Product in its list");

            _repositoryMock.Verify(r => r.AddOrderAsync(It.IsAny<Order>()), Times.Never);
            _unityOfWorkMock.Verify(r => r.CommitChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task UpdateOrder_IdOrderInvalid_ShouldThrowException()
        {
            var nonValidId = 0;
            var products = ObjectFaker.GenerateProductList(0);
            var updateOrderDto = ObjectFaker.GenerateUpdateOrderDTO(nonValidId, products);

            _repositoryMock.Setup(r => r.FindById(nonValidId))
                           .ReturnsAsync((Order?)null);

            var action = async () => await _service.UpdateOrder(updateOrderDto);

            await action.Should().ThrowAsync<OrderNotFoundException>()
                .WithMessage($"Order with id: {nonValidId} was not found");

            _unityOfWorkMock.Verify(r => r.CommitChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task DeleteOrder_ShouldCallRepositoryAndCommit()
        {
            var order = ObjectFaker.GenerateOrder();
            order.Id = 1;

            _repositoryMock.Setup(r => r.FindById(order.Id)).ReturnsAsync(order);

            await _service.DeleteOrder(order.Id);

            _repositoryMock.Verify(r => r.DeleteOrderAsync(order), Times.Once);
            _unityOfWorkMock.Verify(u => u.CommitChangesAsync(), Times.Once);
        }
    }
}
