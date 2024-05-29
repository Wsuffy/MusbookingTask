using Musbooking.Domain.Entities.Equipment;
using Musbooking.Domain.Entities.OrderEquipment;
using Musbooking.Infrastructure.Contexts.Contracts;

namespace Musbooking.Infrastructure.Contexts.Abstractions;

public interface IOrderReadContext : ISavableContext
{
    public IQueryable<Equipment> Equipments { get; }

    public IQueryable<Musbooking.Domain.Entities.Order.Order?> Orders { get; }

    public IQueryable<OrderEquipment> OrderEquipments { get; }
}