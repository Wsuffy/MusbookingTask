using Microsoft.EntityFrameworkCore;
using Musbooking.Infrastructure.Contexts.Contracts;
using Musbooking.Infrastructure.Entities.Equipment;
using Musbooking.Infrastructure.Entities.Order;
using Musbooking.Infrastructure.Entities.OrderEquipment;

namespace Musbooking.Infrastructure.Contexts.Abstractions;

public interface IOrderWriteContext : ISavableContext
{
    public DbSet<Equipment> Equipments { get; }

    public DbSet<Order?> Orders { get; }

    public DbSet<OrderEquipment> OrderEquipments { get; }
}