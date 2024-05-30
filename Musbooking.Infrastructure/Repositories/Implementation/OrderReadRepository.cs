using Microsoft.EntityFrameworkCore;
using Musbooking.Infrastructure.Contexts.Abstractions;
using Musbooking.Infrastructure.Entities.Order;
using Musbooking.Infrastructure.Repositories.Abstractions;

namespace Musbooking.Infrastructure.Repositories.Implementation;

public class OrderReadRepository : IOrderReadRepository
{
    private readonly IOrderReadContext _context;

    public OrderReadRepository(IOrderReadContext context)
    {
        _context = context;
    }

    public async Task<List<Order?>> GetAllAsync(int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        return await _context.Orders
            .Include(o => o.Equipments)
            .ThenInclude(oe => oe.Equipment)
            .OrderBy(x => x.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}