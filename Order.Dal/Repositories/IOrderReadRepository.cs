namespace Order.Dal.Repositories;

public interface IOrderReadRepository
{
    public Task<List<Core.Entities.Order.Order?>> GetAllAsync(int pageNumber, int pageSize,
        CancellationToken cancellationToken);
}