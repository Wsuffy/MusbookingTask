using Musbooking.Dal.Contexts.Contracts;
using Musbooking.Domain.Entities.Equipment;
using Musbooking.Domain.Entities.Order;
using Musbooking.Domain.Entities.OrderEquipment;

namespace Musbooking.Dal.Abstractions;

public interface IOrderReadContext : ISavableContext
{
    public IQueryable<Equipment> Equipments { get; }

    public IQueryable<Order> Orders { get; }

    public IQueryable<OrderEquipment> OrderEquipments { get; }
}