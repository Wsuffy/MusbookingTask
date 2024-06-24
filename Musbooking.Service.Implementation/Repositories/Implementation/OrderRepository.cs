using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Musbooking.Dal.Abstractions;
using Musbooking.Domain.Entities.Order;
using Musbooking.Service.Abstractions.Abstractions;

namespace Musbooking.Service.Implementation.Repositories.Implementation;

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

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        return await _context.BeginTransactionAsync(cancellationToken);
    }
    
}