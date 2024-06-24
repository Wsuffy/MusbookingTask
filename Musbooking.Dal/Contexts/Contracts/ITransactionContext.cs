using Microsoft.EntityFrameworkCore.Storage;

namespace Musbooking.Dal.Contexts.Contracts;

public interface ITransactionContext
{
    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}