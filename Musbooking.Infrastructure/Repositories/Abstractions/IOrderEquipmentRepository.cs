using Musbooking.Infrastructure.Entities.OrderEquipment;

namespace Musbooking.Infrastructure.Repositories.Abstractions;

public interface IOrderEquipmentRepository
{
    public Task<IList<OrderEquipment>> GetAllByOrderId(int orderId, CancellationToken cancellationToken);

    public Task<bool> DeleteRangeAsync(IList<OrderEquipment> orderEquipmentList, CancellationToken cancellationToken);
}