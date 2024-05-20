using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderLink.Sync.Kitchen.Domain.Entities;

namespace OrderLink.Sync.Kitchen.Infrastructure.Mappings
{
    internal class OrderDishMapping : IEntityTypeConfiguration<OrderDish>
    {
        public void Configure(EntityTypeBuilder<OrderDish> builder)
        {
            builder.HasKey(od => od.Id);

            builder.Property(od => od.OrderId)
                .IsRequired();

            builder.Property(od => od.DishId)
                .IsRequired();

            builder.HasOne(od => od.Order)
                .WithMany(o => o.OrderDishes)
                .HasForeignKey(od => od.OrderId);

            builder.HasOne(od => od.Dish)
                .WithMany(d => d.OrderDishes)
                .HasForeignKey(od => od.DishId);

            builder.Property(od => od.CreatedAt)
                .IsRequired();

            builder.Property(od => od.UpdatedAt)
                .IsRequired();

            builder.ToTable("OrderDishes");
        }
    }
}
