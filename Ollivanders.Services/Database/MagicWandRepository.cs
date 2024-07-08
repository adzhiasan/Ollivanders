using Microsoft.EntityFrameworkCore;

namespace Ollivanders.Services.Database;

public sealed class MagicWandRepository
{
    private readonly DatabaseContext _databaseContext;
    private readonly DbSet<MagicWand> _set;

    public MagicWandRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _set = databaseContext.Set<MagicWand>();
    }

    public async Task<MagicWand> CreateAsync(MagicWand magicWand)
    {
        await _set.AddAsync(magicWand);
        await _databaseContext.SaveChangesAsync();

        return magicWand;
    }
    
    public async Task<MagicWand?> TryGetByIdAsync(int id)
    {
        return await _set.Include(mw => mw.Mages).SingleOrDefaultAsync(w => w.Id == id);
    }
    
    public async Task<MagicWand> GetByIdAsync(int id)
    {
        return await _set.SingleAsync(w => w.Id == id);
    }
    
    public async Task UpdateAsync(MagicWand magicWand)
    {
        _set.Update(magicWand);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var magicWand = await GetByIdAsync(id);
        _set.Remove(magicWand);
        
        await _databaseContext.SaveChangesAsync();
    }
}