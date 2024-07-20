using OrderLink.Sync.Core.Messages.Integration.Events;
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
        private readonly IInvokeService _invokeService;
        public OrderService(IHttpClientFactory httpClientFactory, 
                IOrderRepository orderRepository,
                INotificator notificator,
                IInvokeService invokeService) : base(notificator)
        {
            _httpClientFactory = httpClientFactory;
            _orderRepository = orderRepository;
            _invokeService = invokeService;
        }

        public async Task<IEnumerable<DishViewModelData>> GetAllDishesAsync()
        {
            var response = await _httpClientFactory.CreateClient(nameof(OrderService)).GetAsync("api/v1/kitchen/dish");

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
            var orderId = Guid.NewGuid();

            await _invokeService.CreateOrderAsync(new CreateOrderEvent(orderId, orderViewModel.DisheIds, orderViewModel.Note));

            await _orderRepository.AddAsync(new Domain.Entities.Order(orderId));
        }

        public async Task DoneOrderAsync(Guid id)
        {
            var order = await _orderRepository.GetByOrderIdAsync(id);

            if (order == null)
            {
                Notify("Order not found");
                return;
            }

            order.DoneOrder();

            _orderRepository.Update(order);
        }

        public async Task<IEnumerable<OrderResponseViewModel>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();

            return orders.Select(order => new OrderResponseViewModel
            {
                Id = order.Id,
                OrderId = order.OrderId,
                Done = order.Done
            });
        }
    }
}
