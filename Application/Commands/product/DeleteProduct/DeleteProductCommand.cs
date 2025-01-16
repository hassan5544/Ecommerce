using MediatR;

namespace Application.Commands.product.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest;
