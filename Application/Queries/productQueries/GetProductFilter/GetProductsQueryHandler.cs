using Application.Dtos.Product;
using AutoMapper;
using Domain.Entities;
using Domain.filters;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.productQueries.GetProductFilter;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IProductsRepository _productsRepository;
    private readonly IMapper _mapper;
    public GetProductsQueryHandler(IProductsRepository productsRepository, IMapper mapper)
    {
        _productsRepository = productsRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        ProductFilter filter = new ProductFilter
        {
            Category = request.Category,
            priceFrom = request.PriceFrom,
            priceTo = request.PriceTo,
            isAvailable = request.IsAvailable
        };
        var result = await _productsRepository.GetProductsFilter(filter, request.Page, request.PageSize, request.Sort);
        
        return _mapper.Map<IEnumerable<ProductDto>>(result);
    }
}