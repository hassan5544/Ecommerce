using Application.Dtos.Order;
using MediatR;

namespace Application.Commands.order.CreateOrder;

public class CreateOrderCommand : IRequest<Guid>
{
    public Guid CustomerId { get; set; }
    public List<OrderItemDto> Items { get; set; } 
}