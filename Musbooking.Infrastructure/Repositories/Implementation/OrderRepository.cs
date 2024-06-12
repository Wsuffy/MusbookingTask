using Microsoft.EntityFrameworkCore;
using Musbooking.Infrastructure.Contexts.Abstractions;
using Musbooking.Infrastructure.Entities.Order;
using Musbooking.Infrastructure.Repositories.Abstractions;

namespace Musbooking.Infrastructure.Repositories.Implementation;

public class OrderRepository : IOrderRepository
{
    private readonly IOrderWriteContext _context;

    public OrderRepository(IOrderWriteContext context)
    {
        _context = context;
    }


    public async Task AddAndSaveAsync(Order order, CancellationToken cancellationToken)
    {
        await _context.Orders.AddAsync(order, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Orders
            .Include(e => e.Equipments)
            .ThenInclude(oe => oe.Equipment)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task DeleteAndSaveAsync(Order order, CancellationToken cancellationToken)
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
        await _context.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        await _context.CommitTransactionAsync(cancellationToken);
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
    {
        await _context.RollbackTransactionAsync(cancellationToken);
    }
}