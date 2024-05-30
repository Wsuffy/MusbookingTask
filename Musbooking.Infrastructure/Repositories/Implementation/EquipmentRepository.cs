using Microsoft.EntityFrameworkCore;
using Musbooking.Infrastructure.Contexts.Abstractions;
using Musbooking.Infrastructure.Entities.Equipment;
using Musbooking.Infrastructure.Repositories.Abstractions;

namespace Musbooking.Infrastructure.Repositories.Implementation;

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

    public async Task<Equipment> GetByNameAsync(string name,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Equipment>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Equipment equipment,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsyncAndSave(Equipment? equipment,
        CancellationToken cancellationToken)
    {
        _context.Equipments.Remove(equipment);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRangeAsyncAndSave(IEnumerable<Equipment?> equipment,
        CancellationToken cancellationToken)
    {
        _context.Equipments.RemoveRange(equipment);
        await _context.SaveChangesAsync(cancellationToken);
    }
}