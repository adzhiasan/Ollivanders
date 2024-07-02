using FluentAssertions;

namespace Ollivanders.Tests;

public class DragonVeinWandCoreTests
{
    [TestMethod]
    [DataRow(DragonSpecies.HungarianHorntail, 4.5d)]
    [DataRow(DragonSpecies.ChineseFireball, 2.9d)]
    [DataRow(DragonSpecies.RomanianLonghorn, 2d)]
    [DataRow(DragonSpecies.NorwegianRidgeback, 1.84d)]
    public void ShouldReturnMultipliedPriceForConcreteDragonSpecies(DragonSpecies dragonSpecies, double expectedPrice)
    {
        // Act
        var price = new DragonVeinWandCore(dragonSpecies).GetPrice().Value;

        // Assert
        price.Should().BeApproximately(expectedPrice, 0.01);
    }
}