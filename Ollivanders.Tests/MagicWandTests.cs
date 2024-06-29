using FluentAssertions;

namespace Ollivanders.Tests;

[TestClass]
public class MagicWandTests
{
    private const string Core = "DragonVein";
    private const double FlexibilityFactor = 0.05;
    private const int Length = 20;
    private const string Wood = "Oak";
    
    [DataTestMethod]
    [DataRow(17)]
    [DataRow(51)]
    public void ShouldThrowArgumentException_WhenNotValidLength(int length)
    {
        // Act
        var act = () => new MagicWand(length, FlexibilityFactor, Wood, Core);
        
        // Assert
        Assert.ThrowsException<ArgumentException>(act);
    }

    [DataTestMethod]
    [DataRow(0.009)]
    [DataRow(0.21)]
    public void ShouldThrowArgumentException_WhenNotValidFlexibilityFactor(double flexibilityFactor)
    {
        // Act
        var act = () => new MagicWand(Length, flexibilityFactor, Wood, Core);
        
        // Assert
        Assert.ThrowsException<ArgumentException>(act);
    }

    [TestMethod]
    [DataRow("TestWoodType")]
    public void ShouldThrowArgumentException_WhenNotValidWood(string wood)
    {
        // Act
        var act = () => new MagicWand(Length, FlexibilityFactor, wood, Core);
        
        // Assert
        Assert.ThrowsException<ArgumentException>(act);
    }

    [TestMethod]
    [DataRow("TestCore")]
    public void ShouldThrowArgumentException_WhenNotValidCore(string core)
    {
        // Act
        var act = () => new MagicWand(Length, FlexibilityFactor, Wood, core);
        
        // Assert
        Assert.ThrowsException<ArgumentException>(act);
    }

    [TestMethod]
    public void ShouldCreateMagicWand_WhenAllParametersAreValid()
    {
        // Act
        var magicWand = new MagicWand(Length, FlexibilityFactor, Wood, Core);

        // Assert
        Assert.AreEqual(Length, magicWand.Length);
        Assert.AreEqual(FlexibilityFactor, magicWand.FlexibilityFactor);
        Assert.AreEqual(Wood, magicWand.Wood);
        Assert.AreEqual(Core, magicWand.Core);
    }
    
    [TestMethod]
    public void ShouldReturnExpectedPrice_WhenOakWoodAndUnicornCore()
    {
        // Arrange
        var expectedPrice = 2d;

        var magicWand = new MagicWand(
            20,
            0.1,
            "Oak",
            "UnicornHorn");

        // Act
        var actualPrice = magicWand.GetPrice();

        // Assert
        actualPrice.Should().Be(expectedPrice);
    }
}