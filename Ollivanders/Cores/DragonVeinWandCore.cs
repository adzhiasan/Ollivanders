using Ollivanders;

public record DragonVeinWandCore(DragonSpecies DragonSpecies) : WandCore
{
    public DragonVeinWandCore(string dragonSpeciesName) : this(Enum.Parse<DragonSpecies>(dragonSpeciesName))
    {
    }

    public override string Name => CoreNames.DragonVein;

    public override Price GetPrice()
    {
        var basePrice = 2d;

        return DragonSpecies switch
        {
            DragonSpecies.HungarianHorntail => new Price(basePrice * 2.25),
            DragonSpecies.ChineseFireball => new Price(basePrice * 1.45),
            DragonSpecies.RomanianLonghorn => new Price(basePrice * 1.0),
            DragonSpecies.NorwegianRidgeback => new Price(basePrice * 0.92),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

public enum DragonSpecies
{
    HungarianHorntail,
    ChineseFireball,
    RomanianLonghorn,
    NorwegianRidgeback
}