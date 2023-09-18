using Microsoft.EntityFrameworkCore;

namespace CSharpAdvanced.Tpl.Infra;

internal class ProductRepository
{
    private readonly Db _db = new();

    public async Task<List<Product>> GetProducts()
    {
        return await _db.Products.ToListAsync();
    }

    public async Task AddProduct(Product product)
    {
        await _db.Products.AddAsync(product);
        await _db.SaveChangesAsync();
    }
}

internal class ProductSafeRepository
{
    private readonly Db _db = new();
    private readonly object _lockObject = new();

    public async Task<List<Product>> GetProducts()
    {
        return _db.Products.ToList();
    }

    public async Task AddProduct(Product product)
    {
        lock (_lockObject)
        {
            _db.Products.AddAsync(product).AsTask().Wait();
            _db.SaveChangesAsync().Wait();
        }
    }
}