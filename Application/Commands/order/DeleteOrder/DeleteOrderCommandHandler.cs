using Domain.Repositories;
using MediatR;

namespace Application.Commands.order.DeleteOrder;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOrderCommandHandler(IOrdersRepository ordersRepository, IUnitOfWork unitOfWork)
    {
        _ordersRepository = ordersRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        await _ordersRepository.DeleteOrderAsync(request.OrderId, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}