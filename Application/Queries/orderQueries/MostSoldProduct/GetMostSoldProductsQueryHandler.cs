using Application.Dtos.Report;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.orderQueries.MostSoldProduct;

public class GetMostSoldProductsQueryHandler: IRequestHandler<GetMostSoldProductsQuery, IEnumerable<MostSoldProductDto>>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IMapper _mapper;

    public GetMostSoldProductsQueryHandler(IOrdersRepository ordersRepository, IMapper mapper)
    {
        _ordersRepository = ordersRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MostSoldProductDto>> Handle(GetMostSoldProductsQuery request, CancellationToken cancellationToken)
    {
        var result = await _ordersRepository.GetMostSoldProductsAsync(request.TopN, cancellationToken);
        
        return _mapper.Map<IEnumerable<MostSoldProductDto>>(result);
    }
}