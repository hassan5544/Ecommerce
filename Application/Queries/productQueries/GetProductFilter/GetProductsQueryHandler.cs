using Domain.Entities;
using Domain.filters;
using Domain.Repositories;

namespace Application.Queries.productQueries.GetProductFilter;

public class GetProductsQueryHandler
{
    private readonly IProductsRepository _productsRepository;

    public GetProductsQueryHandler(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }
    
    public async Task<IEnumerable<Products>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        ProductFilter filter = new ProductFilter
        {
            Category = request.Category,
            priceFrom = request.PriceFrom,
            priceTo = request.PriceTo,
            isAvailable = request.IsAvailable
        };
        return await _productsRepository.GetProductsFilter(filter, request.Page, request.PageSize, request.Sort);
    }
}