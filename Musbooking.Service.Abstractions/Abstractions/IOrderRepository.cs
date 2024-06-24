using Microsoft.EntityFrameworkCore.Storage;
using Musbooking.Domain.Entities.Order;

namespace Musbooking.Service.Abstractions.Abstractions;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task AddAndSaveAsync(Order order, CancellationToken cancellationToken);
    Task DeleteAndSaveAsync(Order order, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
}