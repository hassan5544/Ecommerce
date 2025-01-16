using MediatR;

namespace Application.Commands.order.DeleteOrder;

public class DeleteOrderCommand: IRequest
{
    public Guid OrderId { get; set; }


}