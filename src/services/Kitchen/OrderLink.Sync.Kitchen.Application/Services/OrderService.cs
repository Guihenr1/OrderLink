using Microsoft.Extensions.Logging;
using OrderLink.Sync.Core.Messages.Integration.Events;
using OrderLink.Sync.Core.Models;
using OrderLink.Sync.Core.Notifications;
using OrderLink.Sync.Kitchen.Application.Interfaces.Repositories;
using OrderLink.Sync.Kitchen.Application.Interfaces.Services;
using OrderLink.Sync.Kitchen.Application.ViewModels.Order;
using OrderLink.Sync.Kitchen.Application.ViewModels.OrderDish;
using System.Transactions;

namespace OrderLink.Sync.Kitchen.Application.Services
{
    public class OrderService : ServiceBase, IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly IDishService _dishService;
        private readonly IOrderDishService _orderDishService;
        private readonly IInvokeService _invokeService;
        public OrderService(ILogger<OrderService> logger,
            INotificator notificator, 
            IOrderRepository orderRepository,
            IDishService dishService,
            IOrderDishService orderDishService,
            IInvokeService invokeService) : base(notificator)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _dishService = dishService;
            _orderDishService = orderDishService;
            _invokeService = invokeService;
        }

        public async Task CreateOrderAsync(OrderRequestViewModel orderRequestViewModel)
        {
            _logger.LogInformation("Creating order");
            
            if (!await CheckDishesExist(orderRequestViewModel.Dishes))
            {
                return;
            }

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await _orderRepository.AddAsync(orderRequestViewModel.ToEntity());

                    foreach (var dishId in orderRequestViewModel.Dishes)
                    {
                        await _orderDishService.AddAsync(new OrderDishViewModel
                        {
                            OrderId = orderRequestViewModel.Id,
                            DishId = dishId
                        });
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating order");
                    throw;
                }
            }
        }

        public async Task DoneDishAsync(Guid orderId)
        {
            await _invokeService.DoneOrderAsync(new DoneOrderEvent(orderId));
        }

        public async Task<IEnumerable<GetAllOrderResponseViewModel>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllOrdersWithOrderDishesAsync();

            return orders.Select(order => new GetAllOrderResponseViewModel
            {
                Id = order.Id,
                Note = order.Note,
                CreatedAt = order.CreatedAt,
                Dishes = order.OrderDishes.Select(orderDish => new GetAllDishesResponseViewModel
                {
                    Id = orderDish.Dish.Id,
                    Name = orderDish.Dish.Name,
                    Description = orderDish.Dish.Description
                }).ToList()
            });
        }

        private async Task<bool> CheckDishesExist(IEnumerable<Guid> dishIds)
        {
            var dishes = await _dishService.GetAllAsync();

            var invalidDishes = dishIds.Where(dishId => !dishes.Any(dish => dish.Id == dishId));

            if (invalidDishes.Any())
            {
                _logger.LogError("Dishes not found: {Dishes}", string.Join(", ", invalidDishes));
                return false;
            }

            return true;
        }
    }
}
