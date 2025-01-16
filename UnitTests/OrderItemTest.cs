using Domain.Entities;

namespace UnitTests;

public class OrderItemTest
{
    [Fact]
    public void CreateOrderItems_ValidParameters_ShouldCreateOrderItem()
    {
        // Arrange
        var product = Products.Create("Laptop", "High-end gaming laptop", 1500.00m, 10, "Electronics", 1);

        // Act
        var orderItem = OrderItems.Create(2, product);

        // Assert
        Assert.NotNull(orderItem);
        Assert.Equal(product.Id, orderItem.ProductId);
        Assert.Equal(2, orderItem.Quantity);
        Assert.Equal(3000.00m, orderItem.TotalAmount);
    }

    [Fact]
    public void CreateOrderItems_InvalidQuantity_ShouldThrowException()
    {
        // Arrange
        var product = Products.Create("Laptop", "High-end gaming laptop", 1500.00m, 10, "Electronics", 1);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => OrderItems.Create(0, product));
    }
}