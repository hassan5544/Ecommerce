namespace Domain.Entities;

public class MostSoldProduct
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int TotalQuantitySold { get; set; }
    public decimal TotalRevenue { get; set; }
}