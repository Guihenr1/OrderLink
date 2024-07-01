using OrderLink.Sync.Kitchen.Application.ViewModels.Order;

namespace OrderLink.Sync.Kitchen.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(OrderRequestViewModel orderRequestViewModel);
        Task<IEnumerable<GetAllOrderResponseViewModel>> GetAllAsync();
        Task DoneDishAsync(Guid orderId);
    }
}
