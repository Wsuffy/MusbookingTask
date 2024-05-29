using Microsoft.EntityFrameworkCore;
using Musbooking.Infrastructure.Contexts.Implementation;
using Musbooking.Infrastructure.Repositories.Abstractions;

namespace Musbooking.Infrastructure.Repositories.Implementation;

public class OrderRepository : IOrderRepository
{
    private readonly OrderWriteContext _context;

    public OrderRepository(OrderWriteContext context)
    {
        _context = context;
    }

    public async Task AddAndSaveAsync(Domain.Entities.Order.Order? order, CancellationToken cancellationToken)
    {
        await _context.Orders.AddAsync(order, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Domain.Entities.Order.Order?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Orders
            .Include(e => e.Equipments)
            .ThenInclude(oe => oe.Equipment)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }


    public async Task UpdateAsync(Domain.Entities.Order.Order order, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAndSaveAsync(Domain.Entities.Order.Order order, CancellationToken cancellationToken)
    {
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken)
    {
        await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        await _context.Database.CommitTransactionAsync(cancellationToken);
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
    {
        await _context.Database.RollbackTransactionAsync(cancellationToken);
    }
}