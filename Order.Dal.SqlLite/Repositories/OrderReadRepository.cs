using Microsoft.EntityFrameworkCore;
using Order.Core.Contexts;
using Order.Dal.Repositories;

namespace Order.Dal.SqlLite.Repositories;

public class OrderReadRepository : IOrderReadRepository
{
    private readonly IOrderReadContext _context;

    public OrderReadRepository(IOrderReadContext context)
    {
        _context = context;
    }

    public async Task<List<Core.Entities.Order.Order?>> GetAllAsync(int pageNumber, int pageSize,
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