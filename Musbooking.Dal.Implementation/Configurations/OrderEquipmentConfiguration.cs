using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Musbooking.Domain.Entities.OrderEquipment;

namespace Musbooking.Dal.Implementation.Configurations;

public class OrderEquipmentConfiguration : IEntityTypeConfiguration<OrderEquipment>
{
    public void Configure(EntityTypeBuilder<OrderEquipment> builder)
    {
        builder.HasKey(oe => new { oe.OrderId, oe.EquipmentId });

        builder.HasOne(oe => oe.Order)
            .WithMany(o => o.Equipments)
            .HasForeignKey(oe => oe.OrderId);

        builder.HasOne(oe => oe.Equipment)
            .WithMany()
            .HasForeignKey(oe => oe.EquipmentId);

        builder.Property(oe => oe.Quantity)
            .IsRequired();
    }
}