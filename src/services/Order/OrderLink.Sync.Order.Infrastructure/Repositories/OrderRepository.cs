using Microsoft.EntityFrameworkCore;
using OrderLink.Sync.Core.Data;
using OrderLink.Sync.Core.Models;
using OrderLink.Sync.Order.Application.Interfaces.Repositories;
using OrderLink.Sync.Order.Infrastructure.Context;

namespace OrderLink.Sync.Order.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Domain.Entities.Order>, IOrderRepository
    {
        private readonly OrderDbContext _context;
        public OrderRepository(OrderDbContext context)
            : base(context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Domain.Entities.Order> GetByOrderIdAsync(Guid orderId)
        {
            return await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId);
        }
    }
}
