using Musbooking.Infrastructure.Contexts.Contracts;
using Musbooking.Infrastructure.Entities.Equipment;
using Musbooking.Infrastructure.Entities.Order;
using Musbooking.Infrastructure.Entities.OrderEquipment;

namespace Musbooking.Infrastructure.Contexts.Abstractions;

public interface IOrderReadContext : ISavableContext
{
    public IQueryable<Equipment> Equipments { get; }

    public IQueryable<Order?> Orders { get; }

    public IQueryable<OrderEquipment> OrderEquipments { get; }
}