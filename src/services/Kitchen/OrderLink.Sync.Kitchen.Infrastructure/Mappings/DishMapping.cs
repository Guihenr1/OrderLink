using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderLink.Sync.Kitchen.Domain.Entities;

namespace OrderLink.Sync.Kitchen.Infrastructure.Mappings
{
    public class DishMapping : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(d => d.Description)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasMany(d => d.OrderDishes)
                .WithOne(od => od.Dish)
                .HasForeignKey(od => od.DishId);

            builder.Property(d => d.CreatedAt)
                .IsRequired();

            builder.Property(d => d.UpdatedAt)
                .IsRequired();

            builder.ToTable("Dishes");
        }
    }
}
