using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.product.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductsRepository _productsRepository;

    public UpdateProductCommandHandler(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }
    
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productsRepository.GetProductByIdAsync(request.Id);
        if (product == null)
        {
            throw new Exception("Product not found");
        }

        product.Update(request.ProductName, request.ProductDescription ,request.Price, request.Stock, request.Category , request.MinQuantity);

        await _productsRepository.UpdateProductAsync(product);
        
    }
    
}