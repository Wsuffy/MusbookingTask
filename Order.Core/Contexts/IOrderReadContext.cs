using Order.Core.Core.Contexts.Contracts;
using Order.Core.Entities.Equipment;
using Order.Core.Entities.OrderEquipment;

namespace Order.Core.Contexts;

public interface IOrderReadContext : ISavableContext
{
    public IQueryable<Equipment> Equipments { get; }

    public IQueryable<Entities.Order.Order?> Orders { get; }

    public IQueryable<OrderEquipment> OrderEquipments { get; }
}