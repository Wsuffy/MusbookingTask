using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Musbooking.Domain.Entities.Equipment;

namespace Musbooking.Infrastructure.Configurations;

public class EquipmentConfiguration : IEntityTypeConfiguration<Equipment>
{
    public void Configure(EntityTypeBuilder<Equipment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(255);
        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.Amount);
        builder.Property(x => x.Price);
    }
}