using Services.API.Entities;

namespace Services.API.Services;

public interface IBasketService
{
    Task<Basket> GetBasketsAsync(Guid byierId);

    Task<Boolean> AddItemToBasketAsync(Guid productId, int quantity, Guid buierId);

    Task<Boolean> RemoveItemFromBasket(Guid productId, int quantity, Guid buierId);

}

