using Domain.Entities;
using Domain.Enums;

namespace UnitTests;

public class OrderTests
{
    [Fact]
    public void CreateOrders_ValidParameters_ShouldCreateOrder()
    {
        // Arrange
        var product = Products.Create("Laptop", "High-end gaming laptop", 1500.00m, 10, "Electronics", 1);
        var orderItems = new List<OrderItems>
        {
            OrderItems.Create(2, product)
        };

        // Act
        var order = Orders.Create(Guid.NewGuid(), orderItems);

        // Assert
        Assert.NotNull(order);
        Assert.Equal(3000.00m, order.TotalAmount);
        Assert.Equal(OrderStatus.Processing, order.Status);
    }

    [Fact]
    public void MarkOrderAsShipped_ValidOrder_ShouldUpdateStatusAndDate()
    {
        // Arrange
        var product = Products.Create("Laptop", "High-end gaming laptop", 1500.00m, 10, "Electronics", 1);
        var orderItems = new List<OrderItems>
        {
            OrderItems.Create(2, product)
        };
        var order = Orders.Create(Guid.NewGuid(), orderItems);

        // Act
        order.MarkAsShipped();

        // Assert
        Assert.Equal(OrderStatus.Shipped, order.Status);
        Assert.NotNull(order.ShippedDate);
    }

    [Fact]
    public void MarkOrderAsShipped_InvalidOrderStatus_ShouldThrowException()
    {
        // Arrange
        var product = Products.Create("Laptop", "High-end gaming laptop", 1500.00m, 10, "Electronics", 1);
        var orderItems = new List<OrderItems>
        {
            OrderItems.Create(2, product)
        };
        var order = Orders.Create(Guid.NewGuid(), orderItems);
        order.MarkAsShipped();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => order.MarkAsShipped());
    }

}