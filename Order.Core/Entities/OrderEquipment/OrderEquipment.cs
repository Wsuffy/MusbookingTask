using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Core.Entities.OrderEquipment;

[Table("OrderEquipment")]
public class OrderEquipment
{
    public int OrderId { get; set; }
    public Order.Order? Order { get; set; }
    public int EquipmentId { get; set; }
    public Equipment.Equipment Equipment { get; set; }
    public int Quantity { get; set; }
}