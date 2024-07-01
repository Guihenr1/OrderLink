using static OrderLink.Sync.Core.Models.AttributeBasedConventions;

namespace OrderLink.Sync.Core.Messages.Integration.Events
{
    [ExchangeName("DoneOrderEventExchange")]
    [QueueName("DoneOrderEventQueue")]
    public class DoneOrderEvent : IntegrationEvent
    {
        public DoneOrderEvent(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; private set; }
    }
}
