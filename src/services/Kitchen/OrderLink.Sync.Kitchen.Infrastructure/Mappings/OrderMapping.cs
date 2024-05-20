using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderLink.Sync.Kitchen.Domain.Entities;

namespace OrderLink.Sync.Kitchen.Infrastructure.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Note)
                .HasMaxLength(255);

            builder.HasMany(o => o.OrderDishes)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId);

            builder.Property(o => o.CreatedAt)
                .IsRequired();

            builder.Property(o => o.UpdatedAt)
                .IsRequired();

            builder.ToTable("Orders");
        }
    }
}
