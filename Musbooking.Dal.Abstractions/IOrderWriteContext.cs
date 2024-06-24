using Microsoft.EntityFrameworkCore;
using Musbooking.Dal.Contexts.Contracts;
using Musbooking.Domain.Entities.Equipment;
using Musbooking.Domain.Entities.Order;
using Musbooking.Domain.Entities.OrderEquipment;

namespace Musbooking.Dal.Abstractions;

public interface IOrderWriteContext : ISavableContext, ITransactionContext
{
    public DbSet<Equipment> Equipments { get; }

    public DbSet<Order> Orders { get; }

    public DbSet<OrderEquipment> OrderEquipments { get; }
}