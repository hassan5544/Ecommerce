using Domain.Repositories;
using MediatR;

namespace Application.Commands.product.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductsRepository _productsRepository;

    public DeleteProductCommandHandler(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }
    
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productsRepository.GetProductByIdAsync(request.Id);
        if (product == null)
            throw new Exception("Product not found");

        await _productsRepository.DeleteProductAsync(request.Id);
    }
}