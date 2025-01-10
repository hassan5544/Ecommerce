using Domain.Shared;

namespace Domain.Entities;

public class OrderItems : BaseEntity
{
    public Products Item { get; private set; }
    public Guid ProductId { get; private set; } // FK
    public Guid OrderId { get; private set; } // Foreign key to Order
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public Products Product { get; private set; }
    public decimal TotalAmount => Quantity * Product.Price;
    

    private OrderItems(int quantity, Products item)
    {
        Id = Guid.NewGuid();
        ProductId = item.Id;
        Price = item.Price;
        Quantity = quantity;
        Item = item;
        Product = item;
    }
    private OrderItems()
    {
        // Required by EF
    }
    
    /// <summary>
    /// Factory method to create a new order item.
    /// </summary>
    /// <param name="quantity"></param>
    /// <param name="item"></param>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public static OrderItems Create(int quantity, Products item)
    {
        if (quantity <= 0)
            throw new InvalidOperationException("Quantity must be greater than zero.");
        if (item == null)
            throw new ArgumentNullException(nameof(item));
        return new OrderItems(quantity, item);
    }

}