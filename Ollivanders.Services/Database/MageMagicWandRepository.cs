using Microsoft.EntityFrameworkCore;

namespace Ollivanders.Services.Database;

public sealed class MageMagicWandRepository
{
    private readonly DatabaseContext _databaseContext;
    private readonly DbSet<MageMagicWand> _set;

    public MageMagicWandRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _set = databaseContext.Set<MageMagicWand>();
    }

    public async Task<MageMagicWand> CreateAsync(MageMagicWand magicWand)
    {
        await _set.AddAsync(magicWand);
        await _databaseContext.SaveChangesAsync();

        return magicWand;
    }
    
    public async Task<IEnumerable<MageMagicWand>> CreateRangeAsync(List<MageMagicWand> magicWand)
    {
        await _set.AddRangeAsync(magicWand);
        await _databaseContext.SaveChangesAsync();

        return magicWand;
    }
    
    public IEnumerable<int> GetMageIdsByMagicWandId(int magicWandId)
    {
        return _set.Where(m => m.MagicWandId == magicWandId).Select(m => m.MageId).AsEnumerable();
    }
    
    public async Task UpdateAsync(MageMagicWand magicWand)
    {
        _set.Update(magicWand);
        await _databaseContext.SaveChangesAsync();
    }
}