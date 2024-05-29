using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Order.Dal.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Core.Entities.Order.Order>
{
    public void Configure(EntityTypeBuilder<Core.Entities.Order.Order> builder)
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