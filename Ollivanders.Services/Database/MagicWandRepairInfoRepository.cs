using Microsoft.EntityFrameworkCore;

namespace Ollivanders.Services.Database;

public class MagicWandRepairInfoRepository
{
    private readonly DatabaseContext _databaseContext;
    private readonly DbSet<MagicWandRepairInfo> _set;

    public MagicWandRepairInfoRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _set = databaseContext.Set<MagicWandRepairInfo>();
    }

    public async Task<MagicWandRepairInfo?> TryGetByMagicWandIdAsync(int magicWandId)
    {
        return await _set.SingleOrDefaultAsync(w => w.MagicWandId == magicWandId);
    }

    public async Task SaveAsync()
    {
        await _databaseContext.SaveChangesAsync();
    }
}