using Musbooking.Domain.Entities.Equipment;

namespace Musbooking.Service.Abstractions.Abstractions;

public interface IEquipmentRepository
{
    public Task AddAsyncAndSave(Equipment equipment, CancellationToken cancellationToken);
    public Task<Equipment?> GetByIdAsync(int id, CancellationToken cancellationToken);

    public Task<Equipment> UpdateAsync(Equipment equipment, string? name, int? amount, decimal? price,
        CancellationToken cancellationToken);

    public Task DeleteAsyncAndSave(Equipment equipment, CancellationToken cancellationToken);

    public Task DeleteRangeAsyncAndSave(IEnumerable<Equipment> equipment,
        CancellationToken cancellationToken);
}