namespace Ollivanders.Services.Controllers;

public sealed class MagicWandCreationDto
{
    public int Length { get; init; }
    public double FlexibilityFactor { get; init; }
    public string Wood { get; init; }
    public string Core { get; init; }

    public MagicWand ToMagicWand()
    {
        return new MagicWand(Length, FlexibilityFactor, Wood, Core);
    }
}