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
    public MagicWand(int length, double flexibilityFactor, WandWood wood, WandCore core)
    {
        if (length is < 18 or > 50)
            throw new ArgumentException(nameof(length));
        if (flexibilityFactor is < 0.01 or > 0.2)
            throw new ArgumentException(nameof(flexibilityFactor));

        Length = length;
        FlexibilityFactor = flexibilityFactor;
        Wood = wood;
        Core = core;
    }

    public int Id { get; set; }
    public int Length { get; init; }
    public double FlexibilityFactor { get; init; }
    public WandWood Wood { get; init; }
    public WandCore Core { get; init; }

    public double GetPrice()
    {
        return (Core.GetPrice() + Wood.GetWoodPrice()).Value;
    }
}