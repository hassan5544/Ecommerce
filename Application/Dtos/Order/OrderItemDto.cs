namespace Application.Dtos.Order;

public record OrderItemDto
{
    public Guid ProductId { get; init; }
    public string ProductName { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
    public decimal TotalAmount { get; init; }
}