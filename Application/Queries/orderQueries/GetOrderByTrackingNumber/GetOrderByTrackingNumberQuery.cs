using Application.Dtos.Order;
using MediatR;

namespace Application.Queries.orderQueries.GetOrderByTrackingNumber;

public class GetOrderByTrackingNumberQuery : IRequest<OrderDto>
{
    public string TrackingNumber { get; set; }
}