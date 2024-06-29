namespace Ollivanders;

public class UnicornMagicWand : MagicWand
{
    public UnicornMagicWand(int length, double flexibilityFactor, string wood, int unicornAge) : base(length,
        flexibilityFactor, wood, "UnicornHorn")
    {
        if (unicornAge is < 20 or > 300)
            throw new ArgumentException(nameof(unicornAge));

        UnicornAge = unicornAge;
    }

    public int UnicornAge { get; init; }

    protected override double GetCorePrice()
    {
        if (UnicornAge > 100)
            return base.GetCorePrice() * (UnicornAge / 100d);
    }

    protected override double GetWoodPrice()
    {
        var basePrice = base.GetPrice();
    }
}

abstract record WandCore(string Name)
{
    protected abstract double GetCorePrice();
}

record UnicornWandCore(int UnicornAge)
    : WandCore(Name)
{
    private const string Name = "UnicornHorn";
    
    protected override double GetCorePrice()
    {
        var basePrice = 1.6d;

        if (UnicornAge > 100)
            return basePrice * (UnicornAge / 100d);
        return basePrice;
    }
}

abstract record WandWood(string Name)
{
    protected abstract double GetWoodPrice();
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

    private readonly IDictionary<string, double> _corePriceDictionary = new Dictionary<string, double>
        (StringComparer.InvariantCultureIgnoreCase)
        {
            { "DragonVein", 2d },
            { "UnicornHorn", 1.6 },
            { "PhoenixFeather", 4d }
        };

    public MagicWand(int length, double flexibilityFactor, string wood, string core)
    {
        if (length is < 18 or > 50)
            throw new ArgumentException(nameof(length));
        if (flexibilityFactor is < 0.01 or > 0.2)
            throw new ArgumentException(nameof(flexibilityFactor));
        if (!_woodPriceDictionary.ContainsKey(wood))
            throw new ArgumentException(nameof(wood));
        if (!_corePriceDictionary.ContainsKey(core))
            throw new ArgumentException(nameof(core));

        Length = length;
        FlexibilityFactor = flexibilityFactor;
        Wood = wood;
        Core = core;
    }

    public int Id { get; set; }
    public int Length { get; init; }
    public double FlexibilityFactor { get; init; }
    public string Wood { get; init; }
    public string Core { get; init; }

    public double GetPrice()
    {
        return GetCorePrice() + GetWoodPrice();
    }

    protected virtual double GetWoodPrice()
    {
        if (_woodPriceDictionary.TryGetValue(Wood, out var woodPrice))
            return woodPrice;
        throw new InvalidOperationException("Object in non-consistent state.");
    }

    protected virtual double GetCorePrice()
    {
        if (_corePriceDictionary.TryGetValue(Core, out var corePrice))
            return corePrice;
        throw new InvalidOperationException("Object in non-consistent state.");
    }
}