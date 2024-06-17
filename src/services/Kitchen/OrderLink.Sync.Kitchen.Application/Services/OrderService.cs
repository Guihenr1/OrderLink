using Microsoft.Extensions.Logging;
using OrderLink.Sync.Core.Models;
using OrderLink.Sync.Core.Notifications;
using OrderLink.Sync.Kitchen.Application.Interfaces.Repositories;
using OrderLink.Sync.Kitchen.Application.Interfaces.Services;
using OrderLink.Sync.Kitchen.Application.ViewModels.Dish;

namespace OrderLink.Sync.Kitchen.Application.Services
{
    public class OrderService : ServiceBase, IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IOrderRepository _orderRepository;
        public OrderService(ILogger<OrderService> logger,
            INotificator notificator, 
            IOrderRepository orderRepository) : base(notificator)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        public async Task CreateOrderAsync(OrderRequestViewModel orderRequestViewModel)
        {
            var id = Guid.NewGuid();

            await _orderRepository.AddAsync(orderRequestViewModel.ToEntity(id));
        }
    }
}
