namespace Domain.filters;

public class ProductFilter
{
    public string Category { get; set; }
    public decimal priceFrom { get; set; }
    public decimal priceTo { get; set; }
    public bool isAvailable { get; set; }
}