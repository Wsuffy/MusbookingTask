using Microsoft.EntityFrameworkCore;
using Musbooking.Dal.Abstractions;
using Musbooking.Domain.Entities.Equipment;
using Musbooking.Service.Abstractions.Abstractions;

namespace Musbooking.Service.Implementation.Repositories.Implementation;

public class EquipmentRepository : IEquipmentRepository
{
    private readonly IOrderWriteContext _context;

    public EquipmentRepository(IOrderWriteContext orderWriteContext)
    {
        _context = orderWriteContext;
    }

    public async Task AddAsyncAndSave(Equipment? equipment, CancellationToken cancellationToken)
    {
        await _context.Equipments.AddAsync(equipment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Equipment?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Equipments
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<Equipment> UpdateAsync(Equipment equipment, string? name, int? amount, decimal? price,
        CancellationToken cancellationToken)
    {
        if (name != null)
            equipment.Name = name;
        if (amount != null)
            equipment.Amount = (int)amount;
        if (price != null)
            equipment.Price = (int)price;

        return equipment;
    }

    public async Task DeleteAsyncAndSave(Equipment equipment,
        CancellationToken cancellationToken)
    {
        _context.Equipments.Remove(equipment);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRangeAsyncAndSave(IEnumerable<Equipment> equipment,
        CancellationToken cancellationToken)
    {
        _context.Equipments.RemoveRange(equipment);
        await _context.SaveChangesAsync(cancellationToken);
    }
}