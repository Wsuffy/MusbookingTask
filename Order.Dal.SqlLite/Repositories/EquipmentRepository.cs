using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Order.Core.Contexts;
using Order.Dal.Repositories;

namespace Order.Dal.SqlLite.Repositories;

public class EquipmentRepository : IEquipmentRepository
{
    private readonly IOrderWriteContext _context;

    public EquipmentRepository(IOrderWriteContext orderWriteContext)
    {
        _context = orderWriteContext;
    }

    public async Task AddAsyncAndSave(Core.Entities.Equipment.Equipment? equipment, CancellationToken cancellationToken)
    {
        await _context.Equipments.AddAsync(equipment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Core.Entities.Equipment.Equipment?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Equipments
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<Core.Entities.Equipment.Equipment> GetByNameAsync(string name,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Core.Entities.Equipment.Equipment>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Core.Entities.Equipment.Equipment equipment, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsyncAndSave(Core.Entities.Equipment.Equipment? equipment,
        CancellationToken cancellationToken)
    {
        _context.Equipments.Remove(equipment);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRangeAsyncAndSave(IEnumerable<Core.Entities.Equipment.Equipment?> equipment,
        CancellationToken cancellationToken)
    {
        _context.Equipments.RemoveRange(equipment);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
}