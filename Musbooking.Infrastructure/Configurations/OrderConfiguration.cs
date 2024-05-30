using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Musbooking.Infrastructure.Entities.Order;

namespace Musbooking.Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.CreatedAt)
            .IsRequired();
        builder.HasIndex(x => x.CreatedAt);

        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.Price);
    }
}