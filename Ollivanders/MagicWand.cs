namespace Ollivanders;

public abstract record WandCore
{
    public abstract string Name { get; }

    public abstract double GetPrice();
}

public record UnicornWandCore : WandCore
{
    public override string Name => "UnicornHorn";

    public int UnicornAge { get; }

    public UnicornWandCore(int unicornAge)
    {
        if (unicornAge is < 20 or > 300)
            throw new ArgumentException(nameof(unicornAge));

        UnicornAge = unicornAge;
    }

    public override double GetPrice()
    {
        var basePrice = 1.6d;

        if (UnicornAge > 100)
            return basePrice * (UnicornAge / 100d);
        return basePrice;
    }
}

public record DragonVeinWandCore(DragonSpecies DragonSpecies) : WandCore
{
    public override string Name => "DragonVein";

    public override double GetPrice()
    {
        var basePrice = 2d;

        return DragonSpecies switch
        {
            DragonSpecies.HungarianHorntail => basePrice * 2.25,
            DragonSpecies.ChineseFireball => basePrice * 1.45,
            DragonSpecies.RomanianLonghorn => basePrice * 1.0,
            DragonSpecies.NorwegianRidgeback => basePrice * 0.92,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

record PhoenixFeatherWandCore(DragonSpecies DragonSpecies) : WandCore
{
    public override string Name => "PhoenixFeather";

    public override double GetPrice() => 4d;
}

public enum DragonSpecies
{
    HungarianHorntail,
    ChineseFireball,
    RomanianLonghorn,
    NorwegianRidgeback
}

public class MagicWand
{
    private readonly IDictionary<string, double> _woodPriceDictionary =
        new Dictionary<string, double>(StringComparer.InvariantCultureIgnoreCase)
        {
            { "Yew", 0.2 },
            { "Ilex", 0.2 },
            { "Cedar", 0.3 },
            { "Oak", 0.4 }
        };


    public MagicWand(int length, double flexibilityFactor, string wood, WandCore core)
    {
        if (length is < 18 or > 50)
            throw new ArgumentException(nameof(length));
        if (flexibilityFactor is < 0.01 or > 0.2)
            throw new ArgumentException(nameof(flexibilityFactor));
        if (!_woodPriceDictionary.ContainsKey(wood))
            throw new ArgumentException(nameof(wood));

        Length = length;
        FlexibilityFactor = flexibilityFactor;
        Wood = wood;
        Core = core;
    }

    public int Id { get; set; }
    public int Length { get; init; }
    public double FlexibilityFactor { get; init; }
    public string Wood { get; init; }
    public WandCore Core { get; init; }

    public double GetPrice()
    {
        return Core.GetPrice() + GetWoodPrice();
    }

    private double GetWoodPrice()
    {
        if (_woodPriceDictionary.TryGetValue(Wood, out var woodPrice))
            return woodPrice;
        throw new InvalidOperationException("Object in non-consistent state.");
    }
}