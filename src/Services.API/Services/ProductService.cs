using Microsoft.EntityFrameworkCore;
using Services.API.Data;
using Services.API.Entities;

namespace Services.API.Services;

public class ProductService : IProductService
{
    private readonly MainDbContext dbContext;

    public ProductService(MainDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<Product> ProductAsync(Guid productId)
    {
        return await dbContext.Products.FindAsync(productId);
    }

    public async Task<IList<Product>> ProductsAsync()
    {
        return await dbContext.Products.ToListAsync();
    }
}

