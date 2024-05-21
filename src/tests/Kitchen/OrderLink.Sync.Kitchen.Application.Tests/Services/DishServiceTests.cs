using Microsoft.Extensions.Logging;
using Moq;
using OrderLink.Sync.Core.Notifications;
using OrderLink.Sync.Kitchen.Application.Interfaces.Repositories;
using OrderLink.Sync.Kitchen.Application.Services;
using OrderLink.Sync.Kitchen.Application.ViewModels.Dish;
using OrderLink.Sync.Kitchen.Domain.Entities;

namespace OrderLink.Sync.Kitchen.Application.Tests.Services
{
    public class DishServiceTests
    {
        private Mock<ILogger<DishService>> _logger;
        private Mock<IDishRepository> _dishRepository;
        private Mock<INotificator> _notificator;
        private DishService _dishService;

        public DishServiceTests()
        {
            _logger = new Mock<ILogger<DishService>>();
            _dishRepository = new Mock<IDishRepository>();
            _notificator = new Mock<INotificator>();

            _dishService = new DishService(_logger.Object, _notificator.Object, _dishRepository.Object);
        }

        [Fact]
        public async Task AddAsync_WhenDishIsValid_ShouldAddDish()
        {
            // Arrange
            var dishViewModel = new DishRequestViewModel
            {
                Name = "Dish Name",
                Description = "Dish Description"
            };

            // Act
            await _dishService.AddAsync(dishViewModel);

            // Assert
            _dishRepository.Verify(r => r.AddAsync(It.IsAny<Dish>()), Times.Once);
        }

        [Fact]
        public async Task AddAsync_WhenDishIsInvalid_ShouldNotAddDish()
        {
            // Arrange
            var dishViewModel = new DishRequestViewModel
            {
                Name = "D",
                Description = "D"
            };

            // Act
            await _dishService.AddAsync(dishViewModel);

            // Assert
            _dishRepository.Verify(r => r.AddAsync(It.IsAny<Dish>()), Times.Never);
        }

        [Fact]
        public async Task GetAllAsync_WhenDishesExists_ShouldReturnDishes()
        {
            // Arrange
            _dishRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Dish>
            {
                new (Guid.NewGuid(), "Dish 1", "Description 1"),
                new (Guid.NewGuid(), "Dish 2", "Description 2")
            });

            // Act
            var dishes = await _dishService.GetAllAsync();

            // Assert
            Assert.StrictEqual(2, dishes.Count());
            _dishRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_WhenDishesNotExists_ShouldReturnEmptyList()
        {
            // Arrange
            _dishRepository.Setup(r => r.GetAllAsync());

            // Act
            var dishes = await _dishService.GetAllAsync();

            // Assert
            Assert.Empty(dishes);
            _dishRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WhenDishExists_ShouldReturnDish()
        {
            // Arrange
            var dishId = Guid.NewGuid();
            var dish = new Dish(dishId, "Dish Name", "Dish Description");
            _dishRepository.Setup(r => r.GetByIdAsync(dishId)).ReturnsAsync(dish);

            // Act
            var result = await _dishService.GetByIdAsync(dishId);

            // Assert
            Assert.NotNull(result);
            Assert.StrictEqual(dishId, result.Id);
            _dishRepository.Verify(r => r.GetByIdAsync(dishId), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WhenDishNotExists_ShouldReturnNull()
        {
            // Arrange
            var dishId = Guid.NewGuid();
            _dishRepository.Setup(r => r.GetByIdAsync(dishId));

            // Act
            var result = await _dishService.GetByIdAsync(dishId);

            // Assert
            Assert.Null(result);
            _dishRepository.Verify(r => r.GetByIdAsync(dishId), Times.Once);
        }

        [Fact]
        public async Task RemoveAsync_WhenDishExists_ShouldRemoveDish()
        {
            // Arrange
            var dishId = Guid.NewGuid();
            var dish = new Dish(dishId, "Dish Name", "Dish Description");
            _dishRepository.Setup(r => r.GetByIdAsync(dishId)).ReturnsAsync(dish);

            // Act
            await _dishService.RemoveAsync(dishId);

            // Assert
            _dishRepository.Verify(r => r.Delete(dish), Times.Once);
        }

        [Fact]
        public async Task RemoveAsync_WhenDishNotExists_ShouldNotRemoveDish()
        {
            // Arrange
            var dishId = Guid.NewGuid();
            _dishRepository.Setup(r => r.GetByIdAsync(dishId));

            // Act
            await _dishService.RemoveAsync(dishId);

            // Assert
            _dishRepository.Verify(r => r.Delete(It.IsAny<Dish>()), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_WhenDishExists_ShouldUpdateDish()
        {
            // Arrange
            var dishId = Guid.NewGuid();
            var dishViewModel = new DishRequestViewModel
            {
                Name = "Dish Name",
                Description = "Dish Description"
            };
            var dish = new Dish(dishId, "Dish Name", "Dish Description");
            _dishRepository.Setup(r => r.GetByIdAsync(dishId)).ReturnsAsync(dish);

            // Act
            await _dishService.UpdateAsync(dishViewModel, dishId);

            // Assert
            _dishRepository.Verify(r => r.Update(It.IsAny<Dish>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenDishNotExists_ShouldNotUpdateDish()
        {
            // Arrange
            var dishId = Guid.NewGuid();
            var dishViewModel = new DishRequestViewModel
            {
                Name = "Dish Name",
                Description = "Dish Description"
            };
            _dishRepository.Setup(r => r.GetByIdAsync(dishId));

            // Act
            await _dishService.UpdateAsync(dishViewModel, dishId);

            // Assert
            _dishRepository.Verify(r => r.Update(It.IsAny<Dish>()), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_WhenDishIsInvalid_ShouldNotUpdateDish()
        {
            // Arrange
            var dishId = Guid.NewGuid();
            var dishViewModel = new DishRequestViewModel
            {
                Name = "D",
                Description = "D"
            };
            var dish = new Dish(dishId, "Dish Name", "Dish Description");
            _dishRepository.Setup(r => r.GetByIdAsync(dishId)).ReturnsAsync(dish);

            // Act
            await _dishService.UpdateAsync(dishViewModel, dishId);

            // Assert
            _dishRepository.Verify(r => r.Update(It.IsAny<Dish>()), Times.Never);
        }
    }
}
