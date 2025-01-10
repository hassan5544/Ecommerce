using Domain.Enums;
using Domain.Shared;

namespace Domain.Entities;

public class Payments : BaseEntity
{
    public Guid OrderId { get; private set; }
    public Orders Order { get; set; } 
    public string PaymentMethod { get; private set; } 
    public decimal Amount { get; private set; }
    public DateTime PaymentDate { get; private set; }
    public PaymentStatus Status { get; private set; } 
    
    /// <summary>
    /// Factory method to create a new payment
    /// </summary>
    /// <param name="paymentMethod"></param>
    /// <param name="amount"></param>
    private Payments( string paymentMethod, decimal amount)
    {
        Id = Guid.NewGuid();
        OrderId = Order.Id;
        PaymentMethod = paymentMethod;
        Amount = amount;
        PaymentDate = DateTime.UtcNow;
        Status = PaymentStatus.Pending;
    }
    
    private Payments()
    {
        // Required by EF
    }
    
    public static Payments Create(string paymentMethod, decimal amount)
    {
        return new Payments(paymentMethod, amount);
    }
}