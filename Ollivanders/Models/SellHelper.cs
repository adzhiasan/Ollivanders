using System.ComponentModel.DataAnnotations;

namespace Ollivanders;

public static class SellHelper
{
    public static MagicWand SellMagicWand(MagicWand magicWand, Mage buyer)
    {
        if (magicWand.IsSold)
            throw new ValidationException($"Magic wand with this id={magicWand.Id} already sold.");
        if (magicWand.Mages.Any() && buyer.DateOfBirth > DateOnly.FromDateTime(DateTime.UtcNow).AddYears(-18))
            throw new ValidationException("Younger than 18 years.");

        magicWand.IsSold = true;
        magicWand.Mages.Add(buyer);

        return magicWand;
    }
}