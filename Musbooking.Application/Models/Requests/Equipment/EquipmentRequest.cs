namespace Musbooking.Application.Models.Requests.Equipment;

public sealed record EquipmentRequest(string Name, int Amount, decimal Price);