using Musbooking.Infrastructure.Entities.Order;

namespace Musbooking.Infrastructure.Repositories.Abstractions;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task AddAndSaveAsync(Order order, CancellationToken cancellationToken);
    Task DeleteAndSaveAsync(Order order, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task BeginTransactionAsync(CancellationToken cancellationToken);
    Task CommitTransactionAsync(CancellationToken cancellationToken);
    Task RollbackTransactionAsync(CancellationToken cancellationToken);
}