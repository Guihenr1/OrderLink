using OrderLink.Sync.Core.Data;
using OrderLink.Sync.Core.Models;
using OrderLink.Sync.Kitchen.Application.Interfaces.Repositories;
using OrderLink.Sync.Kitchen.Domain.Entities;
using OrderLink.Sync.Kitchen.Infrastructure.Context;

namespace OrderLink.Sync.Kitchen.Infrastructure.Repositories
{
    public class OrderDishRepository : RepositoryBase<OrderDish>, IOrderDishRepository
    {
        private readonly KitchenDbContext _context;
        public OrderDishRepository(KitchenDbContext context)
            : base(context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;
    }
}
