using Application.Dtos.Order;
using Domain.Entities;
using MediatR;

namespace Application.Queries.orderQueries.GetOrdersFilter;

public class GetOrderQuery : IRequest<IEnumerable<OrderDto>>
{
    public Guid? CustomerId { get; set; }
    public string? Status { get; set; }
    public DateTime? OrderDateFrom { get; set; }
    public DateTime? OrderDateTo { get; set; }
    public int Page { get; set; } = 1; // Default to page 1
    public int PageSize { get; set; } = 10; // Default page size
    public string Sort { get; set; } = "orderdate"; // Default sort
}