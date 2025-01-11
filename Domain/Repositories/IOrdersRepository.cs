using Domain.Entities;
using Domain.filters;

namespace Domain.Repositories;

public interface IOrdersRepository
{
    Task<Orders> GetOrderByIdAsync(Guid orderId);
    Task AddOrderAsync(Orders order);
    Task<IEnumerable<Orders>> GetOrdersByFilterAsync(OrderFilter filter, int page, int pageSize, string sort);
}