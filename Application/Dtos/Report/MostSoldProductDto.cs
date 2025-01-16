namespace Application.Dtos.Report;

public class MostSoldProductDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int TotalQuantitySold { get; set; }
    public decimal TotalRevenue { get; set; }
}