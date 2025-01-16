using Domain.Repositories;
using MediatR;

namespace Application.Commands.product.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductsRepository _productsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IProductsRepository productsRepository, IUnitOfWork unitOfWork)
    {
        _productsRepository = productsRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productsRepository.GetProductByIdAsync(request.Id);
        if (product == null)
            throw new Exception("Product not found");

        await _productsRepository.DeleteProductAsync(request.Id);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}