using Microsoft.Extensions.Logging;
using Moq;
using OrderLink.Sync.Core.Messages.Integration.Events;
using OrderLink.Sync.Core.Notifications;
using OrderLink.Sync.Kitchen.Application.Interfaces.Repositories;
using OrderLink.Sync.Kitchen.Application.Interfaces.Services;
using OrderLink.Sync.Kitchen.Application.Services;
using OrderLink.Sync.Kitchen.Application.ViewModels.Dish;
using OrderLink.Sync.Kitchen.Application.ViewModels.Order;
using OrderLink.Sync.Kitchen.Application.ViewModels.OrderDish;
using OrderLink.Sync.Kitchen.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderLink.Sync.Kitchen.Application.Tests.Services
{
    public class OrderServiceTests
    {
        private Mock<IOrderRepository> _orderRepository;
        private Mock<IDishService> _dishService;
        private Mock<IOrderDishService> _orderDishService;
        private Mock<IInvokeService> _invokeService;
        private Mock<INotificator> _notificator;
        private Mock<ILogger<OrderService>> _logger;
        private OrderService _orderService;

        public OrderServiceTests()
        {
            _orderRepository = new Mock<IOrderRepository>();
            _dishService = new Mock<IDishService>();
            _orderDishService = new Mock<IOrderDishService>();
            _invokeService = new Mock<IInvokeService>();
            _notificator = new Mock<INotificator>();
            _logger = new Mock<ILogger<OrderService>>();

            _orderService = new OrderService(_logger.Object, _notificator.Object, 
                _orderRepository.Object, _dishService.Object, _orderDishService.Object, 
                _invokeService.Object);
        }

        [Fact]
        public async Task CreateOrderAsync_WhenDishesExist_ShouldCreateOrder()
        {
            // Arrange
            var orderRequestViewModel = new OrderRequestViewModel
            {
                Id = Guid.NewGuid(),
                Dishes = new List<Guid> { Guid.NewGuid() }
            };
            IEnumerable<DishResponseViewModel> list = new List<DishResponseViewModel>
            {
                new DishResponseViewModel { Id = orderRequestViewModel.Dishes.First() }
            };

            // Mocking GetAllAsync method to return a list of dishes
            _dishService.Setup(r => r.GetAllAsync()).Returns(Task.FromResult(list));

            // Act
            await _orderService.CreateOrderAsync(orderRequestViewModel);

            // Assert
            _orderRepository.Verify(r => r.AddAsync(It.IsAny<Order>()), Times.Once);
            _orderDishService.Verify(r => r.AddAsync(It.IsAny<OrderDishViewModel>()), Times.Once);
        }

        [Fact]
        public async Task CreateOrderAsync_WhenDishesDoNotExist_ShouldNotCreateOrder()
        {
            // Arrange
            var orderRequestViewModel = new OrderRequestViewModel
            {
                Id = Guid.NewGuid(),
                Dishes = new List<Guid> { Guid.NewGuid() }
            };
            IEnumerable<DishResponseViewModel> list = new List<DishResponseViewModel>();

            // Mocking GetAllAsync method to return an empty list
            _dishService.Setup(r => r.GetAllAsync()).Returns(Task.FromResult(list));

            // Act
            await _orderService.CreateOrderAsync(orderRequestViewModel);

            // Assert
            _orderRepository.Verify(r => r.AddAsync(It.IsAny<Order>()), Times.Never);
            _orderDishService.Verify(r => r.AddAsync(It.IsAny<OrderDishViewModel>()), Times.Never);
        }

        [Fact]
        public async Task DoneDishAsync_ShouldInvokeDoneOrderAsync()
        {
            // Arrange
            var orderId = Guid.NewGuid();

            // Act
            await _orderService.DoneDishAsync(orderId);

            // Assert
            _invokeService.Verify(r => r.DoneOrderAsync(It.IsAny<DoneOrderEvent>()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllOrders()
        {
            // Arrange
            IEnumerable<Order> list = new List<Order>
            {
                new Order(Guid.NewGuid(), "Note")
                {
                    OrderDishes = new List<OrderDish>
                    {
                        new OrderDish(Guid.NewGuid(), Guid.NewGuid())
                        {
                            Dish = new Dish("Name", "Desc"),
                            Order = new Order(Guid.NewGuid(), "Note")
                        }
                    }
                }
            };

            // Mocking GetAllOrdersWithOrderDishesAsync method to return a list of orders
            _orderRepository.Setup(r => r.GetAllOrdersWithOrderDishesAsync()).Returns(Task.FromResult(list));

            // Act
            var result = await _orderService.GetAllAsync();

            // Assert
            Assert.NotEmpty(result);
        }
    }
}
