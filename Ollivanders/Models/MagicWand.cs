namespace Ollivanders;

public class MagicWand
{
    public MagicWand(
        int length,
        double flexibilityFactor,
        WandWood wood,
        WandCore core,
        List<int> previousOwners,
        Price? basePrice)
    {
        if (length is < 18 or > 50)
            throw new ArgumentException(nameof(length));
        if (flexibilityFactor is < 0.01 or > 0.2)
            throw new ArgumentException(nameof(flexibilityFactor));
        if (core is null && previousOwners.Count == 0)
            throw new ArgumentException("Not collection wands must have core");
        if (basePrice is null && previousOwners.Count == 0)
            throw new ArgumentException("Collection wand must have base price");

        Length = length;
        FlexibilityFactor = flexibilityFactor;
        Wood = wood;
        Core = core;
        MageIds = previousOwners;
        BasePrice = basePrice;
    }

    public int Id { get; set; }
    public int Length { get; init; }
    public double FlexibilityFactor { get; init; }
    public WandWood Wood { get; init; }
    public WandCore? Core { get; init; }
    public List<Mage> Mages { get; set; }
    public List<int> MageIds { get; set; }
    public bool IsSold { get; set; }
    public Price? BasePrice { get; set; }

    public double GetPrice()
    {
        return BasePrice?.Value ?? (Core!.GetPrice() + Wood.GetWoodPrice()).Value;
    }
}