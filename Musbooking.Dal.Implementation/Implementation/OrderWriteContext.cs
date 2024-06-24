using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Musbooking.Dal.Abstractions;
using Musbooking.Dal.Implementation.Configurations;
using Musbooking.Domain.Entities.Equipment;
using Musbooking.Domain.Entities.Order;
using Musbooking.Domain.Entities.OrderEquipment;

namespace Musbooking.Dal.Implementation.Implementation;

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

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default) =>
        await Database.BeginTransactionAsync(cancellationToken);
    
}