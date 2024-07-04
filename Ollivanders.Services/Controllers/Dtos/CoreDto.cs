namespace Ollivanders.Services.Controllers;

public record CoreDto
{
    public int? UnicornAge { get; init; }

    public string? DragonSpecies { get; init; }

    public string CoreName { get; init; }

    public WandCore ToWandCore()
    {
        return CoreName switch
        {
            CoreNames.DragonVein => new DragonVeinWandCore(DragonSpecies),
            CoreNames.UnicornHorn => new UnicornWandCore(UnicornAge.Value),
            CoreNames.PhoenixFeather => new PhoenixFeatherWandCore(),
            _ => throw new ArgumentOutOfRangeException(nameof(CoreName),
                $"Not expected direction value: {CoreName}")
        };
    }
}

public static class WandCoreExtensions
{
    public static CoreDto ToCoreDto(this WandCore wandCore)
    {
        int? unicornAge = 0;
        string? dragonSpecies = null;
        string coreName;

        switch (wandCore.Name)
        {
            case CoreNames.DragonVein:
                dragonSpecies = ((DragonVeinWandCore)wandCore).DragonSpecies.ToString();
                coreName = CoreNames.DragonVein;
                break;
            case CoreNames.UnicornHorn:
                unicornAge = ((UnicornWandCore)wandCore).UnicornAge;
                coreName = CoreNames.UnicornHorn;
                break;
            case CoreNames.PhoenixFeather:
                coreName = CoreNames.PhoenixFeather;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(wandCore.Name));
        }

        return new CoreDto
        {
            UnicornAge = unicornAge,
            DragonSpecies = dragonSpecies,
            CoreName = coreName
        };
    }
}