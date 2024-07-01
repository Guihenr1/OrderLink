using OrderLink.Sync.Core.Models;

namespace OrderLink.Sync.Order.Domain.Entities
{
    public class Order : EntityBase
    {
        public Guid OrderId { get; private set; }
        public bool Done { get; private set; }

        public Order(Guid orderId)
        {
            OrderId = orderId;
            Done = false;
        }

        public void DoneOrder()
        {
            Done = true;
        }
    }
}
