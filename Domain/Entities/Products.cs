using Domain.Shared;

namespace Domain.Entities;

public class Products : BaseEntity
{
    public string ProductName { get; private set; }
    public string? ProductDescription { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public string Category { get; private set; }
    public int MinQuantity { get; private set; }
    
    
    private Products(string name, string? description, decimal price, int stock, string category , int minQuntity)
    {
        Id = Guid.NewGuid();
        ProductName = name;
        ProductDescription = description;
        Price = price;
        Stock = stock;
        Category = category;
        MinQuantity = minQuntity;
    }
    /// <summary>
    /// Factory method to create a new product.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="price"></param>
    /// <param name="stock"></param>
    /// <param name="category"></param>
    /// <param name="minQuntity"></param>
    /// <returns></returns>
    public static Products Create(string name, string? description, decimal price, int stock, string category, int minQuntity)
    {
        return new Products(name, description, price, stock, category, minQuntity);
    }
    private Products()
    {
        // Required by EF
    }

    public void ReduceStock(int quantity)
    {
        if (Stock < quantity)
            throw new InvalidOperationException($"Insufficient stock for product '{ProductName}'.");

        Stock -= quantity;
    }

    public void IncreaseStock(int quantity)
    {
        if(quantity <= 0)
            throw new InvalidOperationException("Quantity must be greater than zero.");
        Stock += quantity;
    }
    
}