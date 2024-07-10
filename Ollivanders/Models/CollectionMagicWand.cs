namespace Ollivanders.Models;

public sealed class CollectionMagicWand
{
    public int Id { get; set; }
    public int Length { get; init; }
    public double FlexibilityFactor { get; init; }
    public WandWood Wood { get; init; }
    public WandCore? Core { get; init; }
    public Price BasePrice { get; init; }
    public List<Mage> PreviousOwners { get; init; }

    public CollectionMagicWand(
        int length,
        double flexibilityFactor,
        WandWood wood,
        WandCore? core,
        List<Mage> previousOwners,
        Price basePrice)
    {
        if (previousOwners.Count == 0)
            throw new ArgumentException("Collection wand must have previous owners.");

        Length = length;
        FlexibilityFactor = flexibilityFactor;
        Wood = wood;
        Core = core;
        PreviousOwners = previousOwners;
        BasePrice = basePrice;
    }
}