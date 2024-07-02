namespace Ollivanders;

public record Price
{
    public double Value { get; set; }

    public Price(double price)
    {
        if (price < 0)
            throw new ArgumentException(nameof(price));

        Value = price;
    }

    public static Price operator +(Price p1, Price p2) =>
        new(p1.Value + p2.Value);
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
        return (Core.GetPrice() + GetWoodPrice()).Value;
    }

    private Price GetWoodPrice()
    {
        if (_woodPriceDictionary.TryGetValue(Wood, out var woodPrice))
            return new Price(woodPrice);
        throw new InvalidOperationException("Object in non-consistent state.");
    }
}