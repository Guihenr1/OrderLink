using OrderLink.Sync.Core.Messages.Integration.Events;

namespace OrderLink.Sync.Kitchen.Application.Interfaces.Services
{
    public interface IConsumerOrderService
    {
        Task CreateOrderAsync(CreateOrderEvent message);
    }
}
