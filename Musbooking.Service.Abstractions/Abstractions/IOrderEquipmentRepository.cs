using Musbooking.Domain.Entities.OrderEquipment;

namespace Musbooking.Service.Abstractions.Abstractions;

public interface IOrderEquipmentRepository
{
    public Task<IReadOnlyList<OrderEquipment>> GetAllByOrderId(int orderId, CancellationToken cancellationToken);

    public Task<bool> DeleteRangeAsync(IReadOnlyList<OrderEquipment> orderEquipmentList, CancellationToken cancellationToken);
}