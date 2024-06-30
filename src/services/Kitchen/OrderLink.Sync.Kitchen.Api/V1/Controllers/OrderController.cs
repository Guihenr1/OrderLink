using Microsoft.AspNetCore.Mvc;
using OrderLink.Sync.Api.Core.Controllers;
using OrderLink.Sync.Core.Notifications;
using OrderLink.Sync.Kitchen.Application.Interfaces.Services;
using OrderLink.Sync.Kitchen.Application.ViewModels.Order;

namespace OrderLink.Sync.Kitchen.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v1/order")]
    public class OrderController : MainController
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;

        public OrderController(
            INotificator notificator,
            ILogger<OrderController> logger,
            IOrderService orderService) : base(notificator)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderRequestViewModel orderViewModel)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                await _orderService.CreateOrderAsync(orderViewModel);

                return CustomResponse();
            } catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return BadRequest();
            }
        }
    }
}
