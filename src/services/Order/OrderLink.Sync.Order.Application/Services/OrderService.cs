using OrderLink.Sync.Core.Models;
using OrderLink.Sync.Core.Notifications;
using OrderLink.Sync.Order.Application.Interfaces.Repositories;
using OrderLink.Sync.Order.Application.Interfaces.Services;
using OrderLink.Sync.Order.Application.ViewModels;

namespace OrderLink.Sync.Order.Application.Services
{
    public class OrderService : ServiceBase, IOrderService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOrderRepository _orderRepository;
        public OrderService(IHttpClientFactory httpClientFactory, IOrderRepository orderRepository,
                            INotificator notificator) : base(notificator)
        {
            _httpClientFactory = httpClientFactory;
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<DishViewModelData>> GetAllDishesAsync()
        {
            var response = await _httpClientFactory.CreateClient(nameof(OrderService)).GetAsync("api/v1/dish");

            if (!response.IsSuccessStatusCode)
            {
                Notify("Error while trying to get all dishes");
                return null;
            }

            var result = await DeserializarObjetoResponse<DishViewModel>(response);

            if (result == null || !result.Success)
            {
                Notify("Error while trying to get the dishes");
                return null;
            }

            return result.Data;
        }

        public async Task AddAsync(OrderRequestViewModel orderViewModel)
        {
            await _orderRepository.AddAsync(new Domain.Entities.Order(Guid.NewGuid()));
        }
    }
}
