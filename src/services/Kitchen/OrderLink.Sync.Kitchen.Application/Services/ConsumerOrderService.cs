using OrderLink.Sync.Core.Messages.Integration.Events;
using OrderLink.Sync.Kitchen.Application.Interfaces.Services;

namespace OrderLink.Sync.Kitchen.Application.Services
{
    public class ConsumerOrderService : IConsumerOrderService
    {
        private readonly IOrderService _orderService;
        public ConsumerOrderService(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task CreateOrderAsync(CreateOrderEvent message)
        {
            await _orderService.CreateOrderAsync(new ViewModels.Order.OrderRequestViewModel()
            {
                Dishes = message.DisheIds,
                Note = message.Note,
                Id = message.OrderId
            });
        }
    }
}
