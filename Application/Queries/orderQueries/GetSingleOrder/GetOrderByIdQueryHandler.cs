using Application.Dtos.Order;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.orderQueries.GetSingleOrder;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IMapper _mapper;

    public GetOrderByIdQueryHandler(IOrdersRepository ordersRepository, IMapper mapper)
    {
        _ordersRepository = ordersRepository;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _ordersRepository.GetOrderByIdAsync(request.OrderId , cancellationToken);
        return _mapper.Map<OrderDto>(order);
    }

}