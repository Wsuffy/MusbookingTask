using Microsoft.EntityFrameworkCore;
using Musbooking.Dal.Abstractions;
using Musbooking.Dal.Implementation.Configurations;
using Musbooking.Domain.Entities.Equipment;
using Musbooking.Domain.Entities.Order;
using Musbooking.Domain.Entities.OrderEquipment;

namespace Musbooking.Dal.Implementation.Implementation;

public class OrderReadContext : DbContext, IOrderReadContext
{
    public IQueryable<Equipment> Equipments => Set<Equipment>().AsNoTracking();

    public IQueryable<Order> Orders => Set<Order>().AsNoTracking();

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