using Domain.Entities;
using Domain.filters;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories;

public class OrderRepository : IOrdersRepository
{
    private readonly ApplicationDbContext _dbContext;

    public OrderRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Orders> GetOrderByIdAsync(Guid orderId)
    {

        var result = await _dbContext.Orders
            .Include(o => o.Items)
            .Include(o => o.Customers)
            .FirstOrDefaultAsync(o => o.Id == orderId);
        
        if(result == null)
            throw new Exception("Order not found");
        return result;
    }

    public async Task AddOrderAsync(Orders order)
    {
        await _dbContext.Orders.AddAsync(order);
        foreach (var item in order.Items)
        {
            await _dbContext.OrderItems.AddAsync(item);
        }
        await _dbContext.SaveChangesAsync();
        
    }

    public async Task UpdateOrderAsync(Orders order)
    {
        _dbContext.Orders.Update(order);
        await _dbContext.SaveChangesAsync();
        
    }

    public async Task DeleteOrderAsync(Guid orderId)
    {
        var order = await _dbContext.Orders.FindAsync(orderId);
        if (order != null)
        {
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Orders>> GetOrdersByFilterAsync(OrderFilter filter, int page, int pageSize, string sort)
    {
        // Validate page and pageSize
        if (page <= 0) page = 1;
        if (pageSize <= 0) pageSize = 10; // Default page size

        // Start building the query
        var query = _dbContext.Orders.AsQueryable();

        // Apply filters
        if (filter.CustomerId != null)
            query = query.Where(x => x.CustomerId == filter.CustomerId);

        if (filter.Status != null)
            query = query.Where(x => x.Status == filter.Status);

        if (filter.OrderDateFrom != null)
            query = query.Where(x => x.OrderDate >= filter.OrderDateFrom);

        if (filter.OrderDateTo != null)
            query = query.Where(x => x.OrderDate <= filter.OrderDateTo);

        // Apply sorting
        query = sort.ToLower() switch
        {
            "orderdate" => query.OrderBy(x => x.OrderDate),
            "orderdate_desc" => query.OrderByDescending(x => x.OrderDate),
            "status" => query.OrderBy(x => x.Status),
            "status_desc" => query.OrderByDescending(x => x.Status),
            _ => query.OrderBy(x => x.Id) // Default sorting by Id
        };

        // Apply pagination
        var result = await query
            .Include(o => o.Items) // Eager load related entities
            .Include(o => o.Customers)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return result;
    }
}