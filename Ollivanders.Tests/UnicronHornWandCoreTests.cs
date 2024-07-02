using FluentAssertions;

namespace Ollivanders.Tests;

[TestClass]
public class UnicornHornWandCoreTests
{
    [TestMethod]
    [DataRow(19)]
    [DataRow(301)]
    public void ShouldThrowException_WhenAgeIsOutOfRange(int unicornAge)
    {
        // Act
        var act = () => new UnicornWandCore(unicornAge);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [TestMethod]
    [DataRow(20)]
    [DataRow(60)]
    [DataRow(100)]
    public void ShouldReturnBasePrice_WhenAgeLessOrEqualTo100(int unicornAge)
    {
        // Act
        var price = new UnicornWandCore(unicornAge).GetPrice().Value;

        // Assert
        price.Should().BeApproximately(1.6d, 0.01);
    }

    [TestMethod]
    [DataRow(142, 2.272)]
    [DataRow(274, 4.384)]
    public void ShouldReturnMultipliedPrice_WhenAgeMoreThan100(int unicornAge, double expectedPrice)
    {
        // Act
        var price = new UnicornWandCore(unicornAge).GetPrice().Value;

        // Assert
        price.Should().BeApproximately(expectedPrice, 0.01);
    }
}