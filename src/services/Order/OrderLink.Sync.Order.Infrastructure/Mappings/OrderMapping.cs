using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderLink.Sync.Order.Infrastructure.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<Domain.Entities.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Order> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.OrderId)
                .IsRequired();

            builder.Property(d => d.Done)
                .IsRequired();

            builder.Property(d => d.CreatedAt)
                .IsRequired();

            builder.Property(d => d.UpdatedAt)
                .IsRequired();

            builder.ToTable("Orders");
        }
    }
}
