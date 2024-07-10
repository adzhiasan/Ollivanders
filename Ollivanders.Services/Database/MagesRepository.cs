using Microsoft.EntityFrameworkCore;

namespace Ollivanders.Services.Database;

public sealed class MagesRepository
{
    private readonly DbSet<Mage> _set;

    public MagesRepository(DatabaseContext databaseContext)
    {
        _set = databaseContext.Set<Mage>();
    }
    
    public async Task<Mage?> TryGetByIdAsync(int id)
    {
        return await _set.SingleOrDefaultAsync(w => w.Id == id);
    }
}