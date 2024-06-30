namespace OrderLink.Sync.Kitchen.Application.ViewModels.OrderDish
{
    public class OrderDishViewModel
    {
        public Guid OrderId { get; set; }
        public Guid DishId { get; set; }

        internal Domain.Entities.OrderDish ToEntity()
        {
            return new Domain.Entities.OrderDish(OrderId, DishId);
        }
    }
}
