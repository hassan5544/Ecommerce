using Domain.Entities;
using Domain.filters;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductsRepository : IProductsRepository
{
    private readonly ApplicationDbContext _context;

    public ProductsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Products> GetProductByIdAsync(Guid productId)
    {
        var result = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        if (result == null)
        {
            throw new Exception("Product not found");
        }

        return result;
    }

    public async Task<List<Products>> GetProductByIdsAsync(IEnumerable<Guid> productsId)
    {
        return await _context.Products.Where(p => productsId.Contains(p.Id)).ToListAsync();
    }

    public async Task<IEnumerable<Products>> GetProductsFilter(ProductFilter filter , int page, int pageSize , string sort)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0) pageSize = 10; 

        
        var query = _context.Products.AsQueryable();

      
        if (!string.IsNullOrWhiteSpace(filter.Category))
            query = query.Where(x => x.Category == filter.Category);

        if (filter.isAvailable)
            query = query.Where(x => x.Stock > 0);

        if (filter.priceFrom > 0)
            query = query.Where(x => x.Price >= filter.priceFrom);

        if (filter.priceTo > 0)
            query = query.Where(x => x.Price <= filter.priceTo);

        // Apply sorting
        query = sort.ToLower() switch
        {
            "price" => query.OrderBy(x => x.Price),
            "price_desc" => query.OrderByDescending(x => x.Price),
            "ProductName" => query.OrderBy(x => x.ProductName),
            "ProductName_desc" => query.OrderByDescending(x => x.ProductName),
            _ => query.OrderBy(x => x.Id) // Default sorting by Id
        };

        // Apply pagination
        var result = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return result;
    }

    public async Task<bool> CheckProductExists(string productName)
    {
        var result = await _context.Products.FirstOrDefaultAsync(p => p.ProductName == productName);

        return result != null;
    }
    public async Task<List<Products>> GetProductsBytermAsync(string term)
    {
        var result = await _context.Products.Where(p => p.ProductName.Contains(term)).ToListAsync();
        return result;
    }

    public async Task AddProductAsync(Products product)
    {
        await _context.Products.AddAsync(product);
    }

    public async Task UpdateProductAsync(Products product)
    {
        _context.Products.Update(product);
    }

    public async Task DeleteProductAsync(Guid productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product != null)
        {
            _context.Products.Remove(product);
        }
    }
}

