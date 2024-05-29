namespace Musbooking.Application.Requests.Order;

public record UpdateOrderRequest(int OrderId, string Description, List<EquipmentRequestWithIdAndQuantity> EquipmentInOrders);