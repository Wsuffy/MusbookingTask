using Musbooking.Infrastructure.Entities.Order;

namespace Musbooking.Infrastructure.Repositories.Abstractions;

public interface IOrderReadRepository
{
    public Task<List<Order?>> GetAllAsync(int pageNumber, int pageSize,
        CancellationToken cancellationToken);
}