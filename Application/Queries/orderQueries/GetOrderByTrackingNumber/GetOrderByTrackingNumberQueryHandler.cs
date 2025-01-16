using Application.Dtos.Order;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.orderQueries.GetOrderByTrackingNumber;

public class GetOrderByTrackingNumberQueryHandler : IRequestHandler<GetOrderByTrackingNumberQuery , OrderDto>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IMapper _mapper;

    public GetOrderByTrackingNumberQueryHandler(IOrdersRepository ordersRepository, IMapper mapper)
    {
        _ordersRepository = ordersRepository;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(GetOrderByTrackingNumberQuery request, CancellationToken cancellationToken)
    {
        var result = await _ordersRepository.GetOrderByTrackingNumber(request.TrackingNumber, cancellationToken);
        return _mapper.Map<OrderDto>(result);
    }
}