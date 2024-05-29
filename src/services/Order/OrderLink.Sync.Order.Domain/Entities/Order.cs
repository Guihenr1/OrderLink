using OrderLink.Sync.Core.Models;

namespace OrderLink.Sync.Order.Domain.Entities
{
    public class Order : EntityBase
    {
        public Guid DishId { get; private set; }
        public bool Done { get; private set; }

        public Order(Guid dishId)
        {
            DishId = dishId;
            Done = false;
        }
    }
}
