using Microsoft.EntityFrameworkCore;
using Order.Core.Contexts;
using Order.Core.Entities.Equipment;
using Order.Core.Entities.OrderEquipment;
using Order.Dal.Configurations;

namespace Order.Dal;

public class OrderReadContext : DbContext, IOrderReadContext
{
    public IQueryable<Equipment> Equipments => Set<Equipment>().AsNoTracking();

    public IQueryable<Core.Entities.Order.Order?> Orders => Set<Core.Entities.Order.Order>().AsNoTracking();

    public IQueryable<OrderEquipment> OrderEquipments => Set<OrderEquipment>().AsNoTracking();

    public OrderReadContext(DbContextOptions<OrderReadContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EquipmentConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderEquipmentConfiguration());
    }
}