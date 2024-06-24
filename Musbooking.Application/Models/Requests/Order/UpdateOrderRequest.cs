namespace Musbooking.Application.Models.Requests.Order;

public sealed record UpdateOrderRequest(int OrderId, string Description, List<EquipmentRequestWithIdAndQuantity> EquipmentInOrders);