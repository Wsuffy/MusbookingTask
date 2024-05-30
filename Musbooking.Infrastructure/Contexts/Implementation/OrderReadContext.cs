using Microsoft.EntityFrameworkCore;
using Musbooking.Infrastructure.Configurations;
using Musbooking.Infrastructure.Contexts.Abstractions;
using Musbooking.Infrastructure.Entities.Equipment;
using Musbooking.Infrastructure.Entities.Order;
using Musbooking.Infrastructure.Entities.OrderEquipment;

namespace Musbooking.Infrastructure.Contexts.Implementation;

public class OrderReadContext : DbContext, IOrderReadContext
{
    public IQueryable<Equipment> Equipments => Set<Equipment>().AsNoTracking();

    public IQueryable<Order?> Orders => Set<Order>().AsNoTracking();

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