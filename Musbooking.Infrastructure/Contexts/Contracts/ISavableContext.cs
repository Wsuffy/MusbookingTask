namespace Musbooking.Infrastructure.Contexts.Contracts;

public interface ISavableContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}