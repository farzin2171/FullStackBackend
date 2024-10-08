using Services.API.Entities;

namespace Services.API.Services;

public interface IProductService
{
    Task<IList<Product>> ProductsAsync();
    Task<Product> ProductAsync(Guid productId);
}

