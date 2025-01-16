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

    public async Task<Orders> GetOrderByIdAsync(Guid orderId , CancellationToken cancellationToken)
    {

        var result = await _dbContext.Orders
            .Include(o => o.Items)
            .Include(o => o.Customers)
            .FirstOrDefaultAsync(o => o.Id == orderId , cancellationToken);
        
        if(result == null)
            throw new Exception("Order not found");
        return result;
    }

    public async Task AddOrderAsync(Orders order , CancellationToken cancellationToken)
    {
        await _dbContext.Orders.AddAsync(order, cancellationToken);
        foreach (var item in order.Items)
        {
            await _dbContext.OrderItems.AddAsync(item , cancellationToken);
            item.Product.ReduceStock(item.Quantity);
            _dbContext.Products.Update(item.Product);
        }
        await _dbContext.SaveChangesAsync(cancellationToken);
        
    }

    public async Task UpdateOrderStatusAsync(Orders order , CancellationToken cancellationToken)
    {
        _dbContext.Orders.Update(order);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
    }

    public async Task DeleteOrderAsync(Guid orderId , CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders.FindAsync(orderId);
        if (order != null)
        {
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<IEnumerable<Orders>> GetOrdersByFilterAsync(OrderFilter filter, int page, int pageSize, string sort , CancellationToken cancellationToken)
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
            .Include(o => o.Customers)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return result;
    }

    public async Task<IEnumerable<MostSoldProduct>> GetMostSoldProductsAsync(int topN, CancellationToken cancellationToken)
    {
       return await _dbContext.OrderItems
        .GroupBy(item => new { item.ProductId, item.Product.ProductName, item.Product.Price })
        .Select(group => new MostSoldProduct
        {
            ProductId = group.Key.ProductId,
            ProductName = group.Key.ProductName,
            TotalQuantitySold = group.Sum(item => item.Quantity),
            TotalRevenue = group.Sum(item => item.Quantity * item.Price)
        })
        .OrderByDescending(dto => dto.TotalQuantitySold)
        .Take(topN)
        .ToListAsync(cancellationToken);
    }

    public async Task<Orders> GetOrderByTrackingNumber(string trackingNumber, CancellationToken cancellationToken)
    {
        var result =  await _dbContext.Orders
            .Include(o => o.Items)
            .Include(o => o.Customers)
            .FirstOrDefaultAsync(o => o.OrderNumber == trackingNumber, cancellationToken); 
        if(result == null)
            throw new Exception("Order not found");
        return result;
    }
}