namespace Ollivanders.Services.Controllers.Dtos;

public sealed record MageDto(string Name, DateOnly DateOfBirth);

internal static class MageDtoExtensions
{
    public static Mage ToMage(this MageDto dto) => new Mage(dto.Name, dto.DateOfBirth);
}

internal static class MageExtensions
{
    public static MageDto ToMageDto(this Mage mage)
    {
        return new MageDto(mage.Name, mage.DateOfBirth);
    }   
}