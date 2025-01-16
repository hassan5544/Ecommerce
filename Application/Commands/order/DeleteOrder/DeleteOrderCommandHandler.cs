using Domain.Repositories;
using MediatR;

namespace Application.Commands.order.DeleteOrder;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IOrdersRepository _ordersRepository;

    public DeleteOrderCommandHandler(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        await _ordersRepository.DeleteOrderAsync(request.OrderId, cancellationToken);
    }
}