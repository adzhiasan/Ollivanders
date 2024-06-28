namespace Ollivanders;

public sealed class MagicWand
{
    private readonly IDictionary<string, double> _woodPriceDictionary = new Dictionary<string, double>
    {
        { "Yew", 0.2 },
        { "Ilex", 0.2 },
        { "Cedar", 0.3 },
        { "Oak", 0.4 }
    };

    private readonly IDictionary<string, double> _corePriceDictionary = new Dictionary<string, double>
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

    public int Length { get; init;  }
    public double FlexibilityFactor { get; init; }
    public string Wood { get; init; }
    public string Core { get; init; }

    public double GetPrice()
    {
        if (_woodPriceDictionary.TryGetValue(Wood, out var woodPrice) &&
            _corePriceDictionary.TryGetValue(Core, out var corePrice))
        {
            return woodPrice + corePrice;
        }

        throw new InvalidOperationException("Object in non-consistent state.");
    }
}