using Microsoft.EntityFrameworkCore;
using Ollivanders.Models;

namespace Ollivanders.Services.Database;

public sealed class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<MagicWand>()
            .ToTable("MagicWands");
        
        modelBuilder
            .Entity<MagicWand>()
            .ToTable("CollectionMagicWands");
        
        modelBuilder
            .Entity<Mage>()
            .ToTable("Mages");
    }
}