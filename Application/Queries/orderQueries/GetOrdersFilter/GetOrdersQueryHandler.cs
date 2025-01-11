using Domain.Entities;
using Domain.Enums;
using Domain.filters;
using Domain.Repositories;

namespace Application.Queries.orderQueries.GetOrdersFilter;

public class GetOrdersQueryHandler
{
    private readonly IOrdersRepository _ordersRepository;

    public GetOrdersQueryHandler(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }
    
    public async Task<IEnumerable<Orders>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        OrderFilter filter = new OrderFilter
        {
            CustomerId = request.CustomerId,
            OrderDateFrom = request.OrderDateFrom,
            Status = Enum.TryParse<OrderStatus>(request.Status, out var status) ? status : null
        };
        return await _ordersRepository.GetOrdersByFilterAsync(filter, request.Page, request.PageSize, request.Sort);
    }
}