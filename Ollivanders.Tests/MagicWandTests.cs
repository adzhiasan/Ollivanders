using FluentAssertions;

namespace Ollivanders.Tests;

[TestClass]
public class MagicWandTests
{
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