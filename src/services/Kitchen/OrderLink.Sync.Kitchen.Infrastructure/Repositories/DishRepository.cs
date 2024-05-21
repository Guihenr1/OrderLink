using OrderLink.Sync.Core.Models;
using OrderLink.Sync.Kitchen.Domain.Entities;
using OrderLink.Sync.Kitchen.Application.Interfaces.Repositories;
using OrderLink.Sync.Kitchen.Infrastructure.Context;
using OrderLink.Sync.Core.Data;

namespace OrderLink.Sync.Kitchen.Infrastructure.Repositories
{
    public class DishRepository : RepositoryBase<Dish>, IDishRepository
    {
        private readonly KitchenDbContext _context;
        public DishRepository(KitchenDbContext context)
            : base(context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;
    }
}
