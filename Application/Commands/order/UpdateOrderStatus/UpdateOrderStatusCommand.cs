using Domain.Enums;
using MediatR;

namespace Application.Commands.order.UpdateOrderStatus;

public class UpdateOrderStatusCommand: IRequest
{
    public Guid OrderId { get; set; }
    public OrderStatus Status { get; set; }
}