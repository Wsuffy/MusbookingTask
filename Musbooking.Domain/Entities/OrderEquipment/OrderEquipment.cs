using System.ComponentModel.DataAnnotations.Schema;

namespace Musbooking.Domain.Entities.OrderEquipment;

[Table("OrderEquipment")]
public class OrderEquipment
{
    public int OrderId { get; set; }
    public Musbooking.Domain.Entities.Order.Order? Order { get; set; }
    public int EquipmentId { get; set; }
    public Equipment.Equipment Equipment { get; set; }
    public int Quantity { get; set; }
}