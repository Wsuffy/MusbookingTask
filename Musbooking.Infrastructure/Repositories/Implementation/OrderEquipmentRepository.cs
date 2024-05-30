using Musbooking.Infrastructure.Contexts.Abstractions;
using Musbooking.Infrastructure.Entities.OrderEquipment;
using Musbooking.Infrastructure.Repositories.Abstractions;

namespace Musbooking.Infrastructure.Repositories.Implementation;

public class OrderEquipmentRepository : IOrderEquipmentRepository
{
    private readonly IOrderWriteContext _context;

    public OrderEquipmentRepository(IOrderWriteContext context)
    {
        _context = context;
    }

    public async Task<IList<OrderEquipment>> GetAllByOrderId(int orderId, CancellationToken cancellationToken)
    {
        return _context.OrderEquipments.Where(x => x.OrderId == orderId).ToList();
    }

    public async Task<bool> DeleteRangeAsync(IList<OrderEquipment> orderEquipmentList,
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