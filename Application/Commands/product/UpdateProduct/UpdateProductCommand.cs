using MediatR;

namespace Application.Commands.product.UpdateProduct;

public record UpdateProductCommand(Guid Id ,string ProductName, string? ProductDescription, decimal Price, int Stock, string Category, int MinQuantity) : IRequest;
