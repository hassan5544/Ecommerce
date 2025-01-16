using Domain.Entities;

namespace UnitTests;

public class ProductTest
{
    [Fact]
    public void ReduceStock_ValidQuantity_ShouldUpdateStock()
    {
        // Arrange
        var product = Products.Create("Laptop", "High-end gaming laptop", 1500.00m, 10, "Electronics", 1);

        // Act
        product.ReduceStock(5);

        // Assert
        Assert.Equal(5, product.Stock);
    }

    [Fact]
    public void ReduceStock_InvalidQuantity_ShouldThrowException()
    {
        // Arrange
        var product = Products.Create("Laptop", "High-end gaming laptop", 1500.00m, 10, "Electronics", 1);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => product.ReduceStock(15));
    }

}