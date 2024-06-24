using Musbooking.Domain.Entities.Order;

namespace Musbooking.Service.Abstractions.Abstractions;

public interface IOrderReadRepository
{
    public Task<List<Order>> GetAllAsync(int pageNumber, int pageSize,
        CancellationToken cancellationToken);
}