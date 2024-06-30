using OrderLink.Sync.Kitchen.Application.ViewModels.OrderDish;

namespace OrderLink.Sync.Kitchen.Application.Interfaces.Services
{
    public interface IOrderDishService
    {
        Task AddAsync(OrderDishViewModel orderDishViewModel);
    }
}
