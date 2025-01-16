using System.Text.Json.Serialization;
using MediatR;

namespace Application.Commands.product.CreateProduct;

public class CreateProductCommand : IRequest<CreateProductResponse>
{
    public string ProductName { get; set; }
    public string? ProductDescription { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string Category { get; set; }
    public int MinQuantity { get; set; }
    
}