using Microsoft.EntityFrameworkCore;
using Musbooking.Domain.Entities.Equipment;
using Musbooking.Domain.Entities.OrderEquipment;
using Musbooking.Infrastructure.Configurations;
using Musbooking.Infrastructure.Contexts.Abstractions;

namespace Musbooking.Infrastructure.Contexts.Implementation;

public class OrderWriteContext : DbContext, IOrderWriteContext
{
    public DbSet<Equipment> Equipments => Set<Equipment>();

    public DbSet<Domain.Entities.Order.Order> Orders => Set<Domain.Entities.Order.Order>();

    public DbSet<OrderEquipment> OrderEquipments => Set<OrderEquipment>();

    public OrderWriteContext(DbContextOptions<OrderWriteContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EquipmentConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderEquipmentConfiguration());
    }
}