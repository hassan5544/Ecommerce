using Domain.Enums;

namespace Domain.filters;

public class OrderFilter
{
    
    public Guid? CustomerId { get; set; }
    public OrderStatus? Status { get; set; }
    public DateTime? OrderDateFrom { get; set; }
    public DateTime? OrderDateTo { get; set; }

    
}