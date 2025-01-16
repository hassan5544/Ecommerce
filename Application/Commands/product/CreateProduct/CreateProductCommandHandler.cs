using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.product.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand , CreateProductResponse>
{
    private readonly IProductsRepository _productRepository;

    public CreateProductCommandHandler(IProductsRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        #region Validate

        if(request.Stock < 0)
        {
            throw new Exception("Stock cannot be negative");
        }
        
        if(request.Price < 0)
        {
            throw new Exception("Price cannot be negative");
        }

        var check = await _productRepository.CheckProductExists(request.ProductName);
        if(check)
        {
            throw new Exception("Product already exists");
        }
        #endregion
        
        var newProduct = Products.Create(request.ProductName , request.ProductDescription , request.Price , request.Stock , request.Category , request.MinQuantity);
        await _productRepository.AddProductAsync(newProduct);
        
        CreateProductResponse response = new CreateProductResponse
        {
            Id = newProduct.Id,
            ProductName = newProduct.ProductName,
            ProductDescription = newProduct.ProductDescription,
            Price = newProduct.Price,
            Stock = newProduct.Stock,
            Category = newProduct.Category,
            MinQuantity = newProduct.MinQuantity
        };

        return response;

    }
}