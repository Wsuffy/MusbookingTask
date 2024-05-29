using Microsoft.EntityFrameworkCore;
using Order.Core.Contexts;
using Order.Core.Entities.Equipment;
using Order.Core.Entities.OrderEquipment;
using Order.Dal.Configurations;

namespace Order.Dal;

public class OrderWriteContext : DbContext, IOrderWriteContext
{
    public DbSet<Equipment?> Equipments => Set<Equipment>();

    public DbSet<Core.Entities.Order.Order?> Orders => Set<Core.Entities.Order.Order>();

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