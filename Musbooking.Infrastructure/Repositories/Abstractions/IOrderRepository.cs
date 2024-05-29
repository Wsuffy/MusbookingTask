namespace Musbooking.Infrastructure.Repositories.Abstractions;

public interface IOrderRepository
{
    Task<Domain.Entities.Order.Order?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task AddAndSaveAsync(Domain.Entities.Order.Order? order, CancellationToken cancellationToken);
    Task UpdateAsync(Domain.Entities.Order.Order order, CancellationToken cancellationToken);
    Task DeleteAndSaveAsync(Domain.Entities.Order.Order order, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task BeginTransactionAsync(CancellationToken cancellationToken);
    Task CommitTransactionAsync(CancellationToken cancellationToken);
    Task RollbackTransactionAsync(CancellationToken cancellationToken);
}