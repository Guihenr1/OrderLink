using OrderLink.Sync.Core.Messages.Integration.Events;
using OrderLink.Sync.Order.Application.Interfaces.Services;

namespace OrderLink.Sync.Order.Application.Services
{
    public class ConsumerOrderService : IConsumerOrderService
    {
        private readonly IOrderService _orderService;
        public ConsumerOrderService(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task DoneOrderAsync(DoneOrderEvent message)
        {
            await _orderService.DoneOrderAsync(message.OrderId);
        }
    }
}
