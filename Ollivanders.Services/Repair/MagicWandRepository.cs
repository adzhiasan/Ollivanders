using Microsoft.EntityFrameworkCore;
using Ollivanders.Services.Database;

namespace Ollivanders.Repair;

public class MagicWandRepository
{
    private readonly DatabaseContext _databaseContext;
    private readonly DbSet<MagicWand> _set;

    public MagicWandRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _set = databaseContext.Set<MagicWand>();
    }

    public async Task<MagicWand?> TryGetByIdAsync(int id) => await _set.SingleOrDefaultAsync(w => w.Id == id);
}