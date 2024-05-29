namespace Musbooking.Application.Requests.Equipment;

public record EquipmentRequest(string Name, int Amount, decimal Price)
{
};