using Domain.Enums;
using Domain.Shared;

namespace Domain.Entities;

public class Orders : BaseEntity
{
    public string OrderNumber { get; init; }
    public DateTime OrderDate { get; init; }
    public string CustomerId { get; init; }
    public List<OrderItems> Items { get; private set; } = new List<OrderItems>();
    public decimal TotalAmount { get; private set; }

    // Tracking fields
    public OrderStatus Status { get; private set; }
    public DateTime? ShippedDate { get; private set; }
    public DateTime? DeliveredDate { get; private set; }
    
    /// <summary>
    /// Factory method to create a new order.
    /// </summary>
    /// <param name="customerId"></param>
    /// <param name="items"></param>
    private Orders(string customerId, IEnumerable<OrderItems> items)
    {
        Id = Guid.NewGuid();
        OrderNumber = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        OrderDate = DateTime.UtcNow;
        CustomerId = customerId;
        AddItems(items);
        Status = OrderStatus.Processing;
    }
    
    private Orders()
    {
        // Required by EF
    }

    public static Orders Create(string customerId, IEnumerable<OrderItems> items )
    {
        return new Orders(customerId, items );
    }
    
    private void AddItems(IEnumerable<OrderItems> items)
    {
        foreach (var item in items)
        {
            Items.Add(item);
            TotalAmount += item.TotalAmount;
        }
    }

    public void MarkAsShipped()
    {
        if (Status != OrderStatus.Processing)
            throw new InvalidOperationException("Only pending orders can be marked as shipped.");

        Status = OrderStatus.Shipped;
        ShippedDate = DateTime.UtcNow;
    }

    public void MarkAsDelivered()
    {
        if (Status != OrderStatus.Shipped)
            throw new InvalidOperationException("Only shipped orders can be marked as delivered.");

        Status = OrderStatus.Delivered;
        DeliveredDate = DateTime.UtcNow;
    }

  
}