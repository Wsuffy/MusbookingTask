namespace Order.Dal.Repositories;

public interface IOrderRepository
{
    Task<Core.Entities.Order.Order?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task AddAndSaveAsync(Core.Entities.Order.Order? order, CancellationToken cancellationToken);
    Task UpdateAsync(Core.Entities.Order.Order order, CancellationToken cancellationToken);
    Task DeleteAndSaveAsync(Core.Entities.Order.Order order, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}