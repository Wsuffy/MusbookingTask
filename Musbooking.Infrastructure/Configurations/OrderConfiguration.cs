using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Musbooking.Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Domain.Entities.Order.Order>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Order.Order> builder)
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