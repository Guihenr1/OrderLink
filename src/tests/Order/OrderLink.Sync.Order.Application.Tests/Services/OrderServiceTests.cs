﻿using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using OrderLink.Sync.Core.Notifications;
using OrderLink.Sync.Order.Application.Interfaces.Repositories;
using OrderLink.Sync.Order.Application.Services;
using OrderLink.Sync.Order.Application.ViewModels;
using System.Net;
using System.Text.Json;

namespace OrderLink.Sync.Order.Application.Tests.Services
{
    public class OrderServiceTests
    {
        private Mock<ILogger<OrderService>> _logger;
        private Mock<IOrderRepository> _orderRepository;
        private Mock<INotificator> _notificator;
        private Mock<IHttpClientFactory> _httpClientFactory;
        private OrderService _orderService;

        public OrderServiceTests()
        {
            _logger = new Mock<ILogger<OrderService>>();
            _orderRepository = new Mock<IOrderRepository>();
            _notificator = new Mock<INotificator>();
            _httpClientFactory = new Mock<IHttpClientFactory>();

            _orderService = new OrderService(_httpClientFactory.Object, _orderRepository.Object, _notificator.Object);
        }

        [Fact]
        public async Task GetAllAsync_WhenDishesExists_ShouldReturnDishes()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(
                    new DishViewModel { Success = true, Data = new List<DishViewModelData> { new DishViewModelData { Name = "Dish Name", Description = "Dish Description" } } }
                ))
            };

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(response);

            var client = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://test.com/")
            };
            _httpClientFactory.Setup(_ => _.CreateClient(nameof(OrderService))).Returns(client);

            // Act
            var result = await _orderService.GetAllDishesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            _httpClientFactory.Verify(f => f.CreateClient(nameof(OrderService)), Times.Once);
        }
    }
}
