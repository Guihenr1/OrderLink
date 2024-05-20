using Microsoft.EntityFrameworkCore;
using OrderLink.Sync.Core.Data;
using OrderLink.Sync.Kitchen.Domain.Entities;
using OrderLink.Sync.Kitchen.Infrastructure.Mappings;

namespace OrderLink.Sync.Kitchen.Infrastructure.Context
{
    public class KitchenDbContext : DbContext, IUnitOfWork
    {
        public KitchenDbContext(DbContextOptions<KitchenDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDish> OrderDishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DishMapping());
            modelBuilder.ApplyConfiguration(new OrderMapping());
            modelBuilder.ApplyConfiguration(new OrderDishMapping());
        }

        public async Task<bool> CommitAsync()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreatedAt").IsModified = false;
                    entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync() > 0;
        }

        public bool Rollback() => false;
    }
}
