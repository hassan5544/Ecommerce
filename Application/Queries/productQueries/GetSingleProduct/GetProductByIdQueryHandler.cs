using Application.Dtos.Product;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.productQueries.GetSingleProduct;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IProductsRepository _productsRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductsRepository productsRepository, IMapper mapper)
    {
        _productsRepository = productsRepository;
        _mapper = mapper;
    }
    
    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _productsRepository.GetProductByIdAsync(request.Id);
        
        return _mapper.Map<ProductDto>(result);
    }
}