namespace Musbooking.Application.Models.Requests.Order;

public sealed record OrderRequest(string Description, List<EquipmentRequestWithIdAndQuantity> EquipmentInOrders);