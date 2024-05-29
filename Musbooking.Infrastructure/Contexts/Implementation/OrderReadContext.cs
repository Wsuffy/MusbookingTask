using Microsoft.EntityFrameworkCore;
using Musbooking.Domain.Entities.Equipment;
using Musbooking.Domain.Entities.OrderEquipment;
using Musbooking.Infrastructure.Configurations;
using Musbooking.Infrastructure.Contexts.Abstractions;

namespace Musbooking.Infrastructure.Contexts.Implementation;

public class OrderReadContext : DbContext, IOrderReadContext
{
    public IQueryable<Equipment> Equipments => Set<Equipment>().AsNoTracking();

    public IQueryable<Domain.Entities.Order.Order?> Orders => Set<Domain.Entities.Order.Order>().AsNoTracking();

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