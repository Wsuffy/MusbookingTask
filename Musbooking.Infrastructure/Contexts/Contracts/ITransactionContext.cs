using Microsoft.EntityFrameworkCore.Storage;

namespace Musbooking.Infrastructure.Contexts.Contracts;

public interface ITransactionContext
{
    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    
    public Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    
    public Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}