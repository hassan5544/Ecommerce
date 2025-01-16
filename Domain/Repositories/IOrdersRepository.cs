using Domain.Entities;
using Domain.Enums;
using Domain.filters;

namespace Domain.Repositories;

public interface IOrdersRepository
{
    Task<Orders> GetOrderByIdAsync(Guid orderId , CancellationToken cancellationToken);
    Task AddOrderAsync(Orders order , CancellationToken cancellationToken);
    Task UpdateOrderStatusAsync(Orders order ,  CancellationToken cancellationToken);
    Task DeleteOrderAsync(Guid orderId , CancellationToken cancellationToken);
    Task<IEnumerable<Orders>> GetOrdersByFilterAsync(OrderFilter filter, int page, int pageSize, string sort , CancellationToken cancellationToken);
    Task<IEnumerable<MostSoldProduct>> GetMostSoldProductsAsync(int topN, CancellationToken cancellationToken);
    Task<Orders> GetOrderByTrackingNumber(string trackingNumber, CancellationToken cancellationToken);
}