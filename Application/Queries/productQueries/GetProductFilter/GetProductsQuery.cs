using Application.Dtos.Product;
using MediatR;

namespace Application.Queries.productQueries.GetProductFilter;

public class GetProductsQuery : IRequest<IEnumerable<ProductDto>>
{
    public string Category { get; set; }
    public bool IsAvailable { get; set; }
    public decimal PriceFrom { get; set; }
    public decimal PriceTo { get; set; }
    public int Page { get; set; } = 1; // Default to page 1
    public int PageSize { get; set; } = 10; // Default page size
    public string Sort { get; set; } = "price"; // Default sort
}