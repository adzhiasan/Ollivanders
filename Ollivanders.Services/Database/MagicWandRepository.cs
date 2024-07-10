using Microsoft.EntityFrameworkCore;
using Ollivanders.Models;

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
        return await _set.SingleOrDefaultAsync(w => w.Id == id);
    }

    public async Task RemoveAsync(MagicWand magicWand)
    {
        _set.Remove(magicWand);
        
        await _databaseContext.SaveChangesAsync();
    }
}