using Microsoft.EntityFrameworkCore;

namespace Ollivanders.Services.Database;

public sealed class MagesRepository
{
    private readonly DatabaseContext _databaseContext;
    private readonly DbSet<Mage> _set;

    public MagesRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _set = databaseContext.Set<Mage>();
    }

    public async Task<Mage> CreateAsync(Mage magicWand)
    {
        await _set.AddAsync(magicWand);
        await _databaseContext.SaveChangesAsync();

        return magicWand;
    }
    
    public async Task<Mage?> TryGetByIdAsync(int id)
    {
        return await _set.SingleOrDefaultAsync(w => w.Id == id);
    }
}