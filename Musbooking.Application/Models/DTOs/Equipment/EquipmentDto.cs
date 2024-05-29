namespace Musbooking.Application.Models.DTOs.Equipment;

public class EquipmentDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }


    public EquipmentDto(global::Musbooking.Domain.Entities.Equipment.Equipment? entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        Amount = entity.Amount;
        Price = entity.Price;
    }

    public EquipmentDto()
    {
    }
}