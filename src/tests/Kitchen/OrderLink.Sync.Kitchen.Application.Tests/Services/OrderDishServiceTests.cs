using Moq;
using OrderLink.Sync.Core.Notifications;
using OrderLink.Sync.Kitchen.Application.Interfaces.Repositories;
using OrderLink.Sync.Kitchen.Application.Services;
using OrderLink.Sync.Kitchen.Application.ViewModels.OrderDish;
using OrderLink.Sync.Kitchen.Domain.Entities;

namespace OrderLink.Sync.Kitchen.Application.Tests.Services
{
    public class OrderDishServiceTests
    {
        private Mock<IOrderDishRepository> _orderDishRepository;
        private Mock<INotificator> _notificator;
        private OrderDishService _orderDishService;
        public OrderDishServiceTests()
        {
            _orderDishRepository = new Mock<IOrderDishRepository>();
            _notificator = new Mock<INotificator>();

            _orderDishService = new OrderDishService(_orderDishRepository.Object, _notificator.Object);
        }

        [Fact]
        public async Task AddAsync_WhenOrderDishIsValid_ShouldAddOrderDish()
        {
            // Arrange
            var orderDishViewModel = new OrderDishViewModel
            {
                OrderId = Guid.NewGuid(),
                DishId = Guid.NewGuid()
            };

            // Act
            await _orderDishService.AddAsync(orderDishViewModel);

            // Assert
            _orderDishRepository.Verify(r => r.AddAsync(It.IsAny<OrderDish>()), Times.Once);
        }
    }
}
