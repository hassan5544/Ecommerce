using Domain.Entities;
using Domain.filters;

namespace Domain.Repositories;

public interface IProductsRepository
{
    Task<Products> GetProductByIdAsync(Guid productId);
    Task<List<Products>> GetProductsBytermAsync(string term);
    Task<IEnumerable<Products>> GetProductsFilter(ProductFilter filter, int page, int pageSize, string sort);
    Task<bool> CheckProductExists(string productName);
    Task AddProductAsync(Products product);
    Task UpdateProductAsync(Products product);
    Task DeleteProductAsync(Guid productId);
}