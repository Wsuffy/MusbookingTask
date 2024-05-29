using Microsoft.EntityFrameworkCore;
using Order.Core.Core.Contexts.Contracts;
using Order.Core.Entities.Equipment;
using Order.Core.Entities.OrderEquipment;

namespace Order.Core.Contexts;

public interface IOrderWriteContext : ISavableContext
{
    public DbSet<Equipment> Equipments { get; }

    public DbSet<Entities.Order.Order?> Orders { get; }

    public DbSet<OrderEquipment> OrderEquipments { get; }
}