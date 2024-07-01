using Microsoft.EntityFrameworkCore;
using OrderLink.Sync.Core.Data;
using OrderLink.Sync.Core.Models;
using OrderLink.Sync.Kitchen.Application.Interfaces.Repositories;
using OrderLink.Sync.Kitchen.Domain.Entities;
using OrderLink.Sync.Kitchen.Infrastructure.Context;

namespace OrderLink.Sync.Kitchen.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        private readonly KitchenDbContext _context;
        public OrderRepository(KitchenDbContext context)
            : base(context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Order>> GetAllOrdersWithOrderDishesAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderDishes)
                .ThenInclude(od => od.Dish)
                .OrderBy(o => o.CreatedAt)
                .ToListAsync();
        }
    }
}
