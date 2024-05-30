using Microsoft.EntityFrameworkCore;
using Musbooking.Infrastructure.Configurations;
using Musbooking.Infrastructure.Contexts.Abstractions;
using Musbooking.Infrastructure.Entities.Equipment;
using Musbooking.Infrastructure.Entities.Order;
using Musbooking.Infrastructure.Entities.OrderEquipment;

namespace Musbooking.Infrastructure.Contexts.Implementation;

public class OrderWriteContext : DbContext, IOrderWriteContext
{
    public DbSet<Equipment> Equipments => Set<Equipment>();

    public DbSet<Order> Orders => Set<Order>();

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