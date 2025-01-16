using Application.Dtos.Order;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.filters;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.orderQueries.GetOrdersFilter;

public class GetOrdersQueryHandler : IRequestHandler<GetOrderQuery, IEnumerable<OrderDto>>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IMapper _mapper;

    public GetOrdersQueryHandler(IOrdersRepository ordersRepository, IMapper mapper)
    {
        _ordersRepository = ordersRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<OrderDto>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        OrderFilter filter = new OrderFilter
        {
            CustomerId = request.CustomerId,
            OrderDateFrom = request.OrderDateFrom,
            Status = Enum.TryParse<OrderStatus>(request.Status, out var status) ? status : null
        };
        var result = await _ordersRepository.GetOrdersByFilterAsync(filter, request.Page, request.PageSize, request.Sort ,cancellationToken);
        return _mapper.Map<IEnumerable<OrderDto>>(result);
    }
}