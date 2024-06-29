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

    public async Task<int> CreateAsync(MagicWand magicWand)
    {
        await _set.AddAsync(magicWand);
        await _databaseContext.SaveChangesAsync();

        return magicWand.Id;
    }
    
    public async Task<MagicWand?> TryGetByIdAsync(int id)
    {
        return await _set.SingleOrDefaultAsync(w => w.Id == id);
    }
}