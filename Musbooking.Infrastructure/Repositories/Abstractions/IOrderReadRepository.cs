namespace Musbooking.Infrastructure.Repositories.Abstractions;

public interface IOrderReadRepository
{
    public Task<List<Domain.Entities.Order.Order?>> GetAllAsync(int pageNumber, int pageSize,
        CancellationToken cancellationToken);
}