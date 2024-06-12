using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default) =>
        await Database.BeginTransactionAsync(cancellationToken);

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default) =>
        await Database.CommitTransactionAsync(cancellationToken);

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default) =>
        await Database.RollbackTransactionAsync(cancellationToken);
}