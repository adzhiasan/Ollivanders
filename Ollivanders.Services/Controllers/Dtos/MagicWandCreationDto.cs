namespace Ollivanders.Services.Controllers;

public sealed class MagicWandCreationDto
{
    public int Length { get; init; }
    public double FlexibilityFactor { get; init; }
    public string WoodName { get; init; }
    public CoreDto Core { get; init; }

    public MagicWand ToMagicWand() =>
        new MagicWand(Length, FlexibilityFactor, new WandWood(WoodName), Core.ToWandCore());
}