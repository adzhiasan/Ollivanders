using Microsoft.EntityFrameworkCore;
using Ollivanders.Services.Database;

namespace Ollivanders.Repair;

public class RepairRequestRepository
{
    private readonly DatabaseContext _databaseContext;
    private readonly DbSet<RepairRequest> _set;

    public RepairRequestRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _set = databaseContext.Set<RepairRequest>();
    }

    public async Task<RepairRequest> CreateAsync(RepairRequest repairRequest)
    {
        await _set.AddAsync(repairRequest);
        await _databaseContext.SaveChangesAsync();

        return repairRequest;
    }

    public async Task<IEnumerable<RepairRequest>> TryGetByMagicWandIdAsync(int magicWandId) =>
        await _set.Where(w => w.MagicWandId == magicWandId).ToListAsync();
}