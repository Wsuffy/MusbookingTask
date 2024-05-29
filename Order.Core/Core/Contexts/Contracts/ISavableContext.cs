namespace Order.Core.Core.Contexts.Contracts;

public interface ISavableContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}