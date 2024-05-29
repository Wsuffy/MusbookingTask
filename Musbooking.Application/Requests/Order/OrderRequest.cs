namespace Musbooking.Application.Requests.Order;

public record OrderRequest(string Description, List<EquipmentRequestWithIdAndQuantity> EquipmentInOrders);

public record EquipmentRequestWithIdAndQuantity(int Id, int Quantity);