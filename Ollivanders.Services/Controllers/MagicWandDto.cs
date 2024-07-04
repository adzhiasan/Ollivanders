namespace Ollivanders.Services.Controllers;

public class MagicWandRequestDto
{
    public int Length { get; init; }
    public double FlexibilityFactor { get; init; }
    public required string Wood { get; init; }
    public required CoreDto Core { get; init; }

    public MagicWand ToMagicWand()
    {
        return new MagicWand(Length, FlexibilityFactor, new WandWood(Wood), Core.ToWandCore());
    }
}

public sealed class MagicWandResponseDto : MagicWandRequestDto
{
    public double Price { get; init; }
}

internal static class MagicWandExtensions
{
    public static MagicWandResponseDto ToMagicWandResponseDto(this MagicWand magicWand)
    {
        return new MagicWandResponseDto
        {
            Length = magicWand.Length,
            FlexibilityFactor = magicWand.FlexibilityFactor,
            Wood = magicWand.Wood.Name,
            Core = magicWand.Core.ToCoreDto(),
            Price = magicWand.GetPrice()
        };
    }
}