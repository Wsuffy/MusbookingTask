using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Order.Core.Contexts;
using Order.Dal.Repositories;

namespace Order.Dal.SqlLite.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IOrderWriteContext _context;

    public OrderRepository(IOrderWriteContext context)
    {
        _context = context;
    }

    public async Task AddAndSaveAsync(Core.Entities.Order.Order? order, CancellationToken cancellationToken)
    {
        await _context.Orders.AddAsync(order, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Core.Entities.Order.Order?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Orders
            .Include(e => e.Equipments)
            .ThenInclude(oe => oe.Equipment)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    

    public async Task UpdateAsync(Core.Entities.Order.Order order, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAndSaveAsync(Core.Entities.Order.Order order, CancellationToken cancellationToken)
    {
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}