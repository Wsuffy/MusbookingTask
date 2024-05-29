using Microsoft.EntityFrameworkCore;
using Musbooking.Domain.Entities.Equipment;
using Musbooking.Domain.Entities.OrderEquipment;
using Musbooking.Infrastructure.Contexts.Contracts;

namespace Musbooking.Infrastructure.Contexts.Abstractions;

public interface IOrderWriteContext : ISavableContext
{
    public DbSet<Equipment> Equipments { get; }

    public DbSet<Musbooking.Domain.Entities.Order.Order?> Orders { get; }

    public DbSet<OrderEquipment> OrderEquipments { get; }
}