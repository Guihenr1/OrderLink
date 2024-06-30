using OrderLink.Sync.Core.Models;
using OrderLink.Sync.Core.Notifications;
using OrderLink.Sync.Kitchen.Application.Interfaces.Repositories;
using OrderLink.Sync.Kitchen.Application.Interfaces.Services;
using OrderLink.Sync.Kitchen.Application.ViewModels.OrderDish;

namespace OrderLink.Sync.Kitchen.Application.Services
{
    public class OrderDishService : ServiceBase, IOrderDishService
    {
        private readonly IOrderDishRepository _orderDishRepository;
        public OrderDishService(IOrderDishRepository orderDishRepository,
                            INotificator notificator) : base(notificator)
        {
            _orderDishRepository = orderDishRepository;
        }

        public async Task AddAsync(OrderDishViewModel orderDishViewModel)
        {
            await _orderDishRepository.AddAsync(orderDishViewModel.ToEntity());
        }
    }
}
