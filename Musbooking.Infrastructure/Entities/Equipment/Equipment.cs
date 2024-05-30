using System.ComponentModel.DataAnnotations.Schema;

namespace Musbooking.Infrastructure.Entities.Equipment;

[Table("Equipments")]
public class Equipment
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }
}