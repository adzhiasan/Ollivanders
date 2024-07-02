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