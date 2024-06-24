using System.ComponentModel.DataAnnotations.Schema;

namespace Musbooking.Domain.Entities.Order;

[Table("Orders")]
public class Order
{
    public int Id { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public decimal Price { get; set; }

    public List<OrderEquipment.OrderEquipment> Equipments { get; set; } = new();
}