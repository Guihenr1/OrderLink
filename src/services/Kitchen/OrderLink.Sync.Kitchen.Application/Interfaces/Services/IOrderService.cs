using OrderLink.Sync.Kitchen.Application.ViewModels.Dish;

namespace OrderLink.Sync.Kitchen.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(OrderRequestViewModel orderRequestViewModel);
    }
}
