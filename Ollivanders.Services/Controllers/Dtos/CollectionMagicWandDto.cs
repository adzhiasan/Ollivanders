using Ollivanders.Models;

namespace Ollivanders.Services.Controllers.Dtos;

public class CollectionMagicWandRequestDto
{
    public int Length { get; init; }
    public double FlexibilityFactor { get; init; }
    public required string Wood { get; init; }
    public required CoreDto? Core { get; init; }
    public List<MageDto> PreviousOwners { get; init; }
    public Price BasePrice { get; init; }

    public CollectionMagicWand ToCollectionMagicWand()
    {
        return new CollectionMagicWand(
            Length,
            FlexibilityFactor,
            new WandWood(Wood),
            Core?.ToWandCore(),
            PreviousOwners.Select(m => m.ToMage()).ToList(),
            BasePrice);
    }
}

public sealed class CollectionMagicWandResponseDto
{
    public int Length { get; init; }
    public double FlexibilityFactor { get; init; }
    public required string Wood { get; init; }
    public required CoreDto? Core { get; init; }
    public List<MageDto> PreviousOwners { get; init; }
    public double Price { get; init; }
}

internal static class CollectionMagicWandExtensions
{
    public static CollectionMagicWandResponseDto ToMagicWandResponseDto(this CollectionMagicWand magicWand)
    {
        return new CollectionMagicWandResponseDto
        {
            Length = magicWand.Length,
            FlexibilityFactor = magicWand.FlexibilityFactor,
            Wood = magicWand.Wood.Name,
            Core = magicWand.Core?.ToCoreDto(),
            Price = magicWand.BasePrice.Value,
            PreviousOwners = magicWand.PreviousOwners.Select(m => m.ToMageDto()).ToList(),
        };
    }
}