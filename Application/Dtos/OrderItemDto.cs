namespace Application.Dtos;

public record OrderItemDto
{
    public Guid ProductId { get; init; }
    public string ProductName { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
    public decimal Subtotal { get; init; }
}