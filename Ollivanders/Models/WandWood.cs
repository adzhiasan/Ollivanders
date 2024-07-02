namespace Ollivanders;

public sealed class WandWood
{
    private readonly IDictionary<string, double> _woodPriceDictionary =
        new Dictionary<string, double>(StringComparer.InvariantCultureIgnoreCase)
        {
            { "Yew", 0.2 },
            { "Ilex", 0.2 },
            { "Cedar", 0.3 },
            { "Oak", 0.4 }
        };

    public WandWood(string woodName)
    {
        if (!_woodPriceDictionary.ContainsKey(woodName))
            throw new ArgumentException(nameof(woodName));

        Name = woodName;
    }

    public string Name { get; set; }

    public Price GetWoodPrice()
    {
        if (_woodPriceDictionary.TryGetValue(Name, out var woodPrice))
            return new Price(woodPrice);
        throw new InvalidOperationException("Object in non-consistent state.");
    }
}