using Ollivanders;

public record UnicornWandCore : WandCore
{
    public override string Name => CoreNames.UnicornHorn;

    public int UnicornAge { get; }

    public UnicornWandCore(int unicornAge)
    {
        if (unicornAge is < 20 or > 300)
            throw new ArgumentException(nameof(unicornAge));

        UnicornAge = unicornAge;
    }

    public override Price GetPrice()
    {
        var basePrice = 1.6d;

        if (UnicornAge > 100)
            return new Price(basePrice * (UnicornAge / 100d));
        return new Price(basePrice);
    }
}