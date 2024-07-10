using Microsoft.EntityFrameworkCore;
using Ollivanders.Models;

namespace Ollivanders.Services.Database;

public sealed class CollectionMagicWandRepository
{
    private readonly DatabaseContext _databaseContext;
    private readonly DbSet<CollectionMagicWand> _set;

    public CollectionMagicWandRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _set = databaseContext.Set<CollectionMagicWand>();
    }

    public async Task<CollectionMagicWand> CreateAsync(CollectionMagicWand magicWand)
    {
        await _set.AddAsync(magicWand);
        await _databaseContext.SaveChangesAsync();

        return magicWand;
    }

    public async Task<CollectionMagicWand?> TryGetByIdAsync(int id)
    {
        return await _set
            .Include(mw => mw.PreviousOwners)
            .SingleOrDefaultAsync(w => w.Id == id);
    }

    public async Task RemoveAsync(CollectionMagicWand collectionMagicWand)
    {
        _set.Remove(collectionMagicWand);

        await _databaseContext.SaveChangesAsync();
    }
}