using OrderLink.Sync.Core.Models;

namespace OrderLink.Sync.Kitchen.Domain.Entities
{
    public class OrderDish : EntityBase
    {
        public Guid OrderId { get; private set; }
        public Order Order { get; set; }

        public Guid DishId { get; private set; }
        public Dish Dish { get; set; }

        public OrderDish(Guid orderId, Guid dishId)
        {
            OrderId = orderId;
            DishId = dishId;
        }
    }
}
