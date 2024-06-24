using Musbooking.Dal.Abstractions;
using Musbooking.Domain.Entities.OrderEquipment;
using Musbooking.Service.Abstractions.Abstractions;

namespace Musbooking.Service.Implementation.Repositories.Implementation;

public class OrderEquipmentRepository : IOrderEquipmentRepository
{
    private readonly IOrderWriteContext _context;

    public OrderEquipmentRepository(IOrderWriteContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<OrderEquipment>> GetAllByOrderId(int orderId, CancellationToken cancellationToken)
    {
        return _context.OrderEquipments.Where(x => x.OrderId == orderId).ToList();
    }

    public async Task<bool> DeleteRangeAsync(IReadOnlyList<OrderEquipment> orderEquipmentList,
        CancellationToken cancellationToken)
    {
        try
        {
            _context.OrderEquipments.RemoveRange(orderEquipmentList);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}