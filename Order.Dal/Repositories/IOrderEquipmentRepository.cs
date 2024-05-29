using Order.Core.Entities.OrderEquipment;

namespace Order.Dal.Repositories;

public interface IOrderEquipmentRepository
{
    public Task<IList<OrderEquipment>> GetAllByOrderId(int orderId, CancellationToken cancellationToken);

    public Task<bool> DeleteRangeAsync(IList<OrderEquipment> orderEquipmentList, CancellationToken cancellationToken);
}