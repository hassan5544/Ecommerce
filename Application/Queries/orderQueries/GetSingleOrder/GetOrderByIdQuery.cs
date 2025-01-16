using Application.Dtos.Order;
using MediatR;

namespace Application.Queries.orderQueries.GetSingleOrder;

public class GetOrderByIdQuery: IRequest<OrderDto>
{
    public Guid OrderId { get; set; }

    public GetOrderByIdQuery(Guid orderId)
    {
        OrderId = orderId;
    }

}