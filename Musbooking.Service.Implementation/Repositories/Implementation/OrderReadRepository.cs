using Microsoft.EntityFrameworkCore;
using Musbooking.Dal.Abstractions;
using Musbooking.Domain.Entities.Order;
using Musbooking.Service.Abstractions.Abstractions;

namespace Musbooking.Service.Implementation.Repositories.Implementation;

public class OrderReadRepository : IOrderReadRepository
{
    private readonly IOrderReadContext _context;

    public OrderReadRepository(IOrderReadContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetAllAsync(int pageNumber, int pageSize,
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