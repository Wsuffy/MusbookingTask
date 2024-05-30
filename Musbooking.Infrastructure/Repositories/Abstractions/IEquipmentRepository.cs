using Musbooking.Infrastructure.Entities.Equipment;

namespace Musbooking.Infrastructure.Repositories.Abstractions;

public interface IEquipmentRepository
{
    public Task AddAsyncAndSave(Equipment? equipment, CancellationToken cancellationToken);
    public Task<Equipment?> GetByIdAsync(int id, CancellationToken cancellationToken);
    public Task<Equipment> GetByNameAsync(string name, CancellationToken cancellationToken);
    public Task<List<Equipment>> GetAllAsync(CancellationToken cancellationToken);
    public Task UpdateAsync(Equipment equipment, CancellationToken cancellationToken);
    public Task DeleteAsyncAndSave(Equipment? equipment, CancellationToken cancellationToken);

    public Task DeleteRangeAsyncAndSave(IEnumerable<Equipment?> equipment,
        CancellationToken cancellationToken);
}