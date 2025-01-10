namespace Application.Dtos;

public record OrderDto
{
    public string OrderNumber { get; init; }
    public DateTime OrderDate { get; init; }
    public string CustomerId { get; init; }
    public decimal TotalAmount { get; init; }

    public string Status { get; init; }
    public DateTime? ShippedDate { get; init; }
    public DateTime? DeliveredDate { get; init; }
    public List<OrderItemDto> Items { get; init; } = new List<OrderItemDto>();
}