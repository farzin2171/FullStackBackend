using Microsoft.EntityFrameworkCore;
using Services.API.Data;
using Services.API.Entities;

namespace Services.API.Services;

public class BasketService : IBasketService
{
    private readonly MainDbContext dbContext;

    public BasketService(MainDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<Boolean> AddItemToBasketAsync(Guid productId, int quantity, Guid buyerId)
    {
        var basket = await GetBasketsAsync(buyerId);

        if (basket == null)
        {
            basket = CreateBasket(buyerId);
        }

        var product = await dbContext.Products.FindAsync(productId);
        //ToDo:error handling

        basket.AddItem(product, quantity);

        return await dbContext.SaveChangesAsync() > 0;

    }

    public async Task<Basket> GetBasketsAsync(Guid byierId)
    {
        return await dbContext.Baskets
                .Include(i => i.Items)
                .ThenInclude(p => p.Product)
                         .FirstOrDefaultAsync(x => x.BuyerId == byierId);
    }

    public async Task<Boolean> RemoveItemFromBasket(Guid productId, int quantity, Guid buyerId)
    {
        var basket = await GetBasketsAsync(buyerId);

        //ToDo: handle error

        basket.RemoveItem(productId, quantity);

        return await dbContext.SaveChangesAsync() > 0;

    }

    private Basket CreateBasket(Guid buyerId)
    {
        var basket = new Basket { BuyerId = buyerId };
        dbContext.Baskets.Add(basket);

        return basket;
    }
}

