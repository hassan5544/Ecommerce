using Domain.Enums;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.order.UpdateOrderStatus;

public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand>
{
    private readonly IOrdersRepository _ordersRepository;

    public UpdateOrderStatusCommandHandler(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public async Task Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await _ordersRepository.GetOrderByIdAsync(request.OrderId, cancellationToken);
        if (order == null)
            throw new Exception("Order not found");
        
        switch (request.Status)
        {
            case OrderStatus.Shipped:
                order.MarkAsShipped();
                break;
            case OrderStatus.Delivered:
                order.MarkAsDelivered();
                break;
        }
        
        await _ordersRepository.UpdateOrderStatusAsync(order , cancellationToken);
    }
}