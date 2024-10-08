using Services.API.Entities;

namespace Services.API.Dtos
{
    public class BasketDto
    {
        public Guid Id { get; set; }
        public Guid BuyerId { get; set; }

        public List<BasketItemDto> Items { get; set; }

        public static BasketDto MapDTO(Basket basket)
        {
            return new BasketDto
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId,
                Items = basket.Items.Select(item => new BasketItemDto
                {
                    ProductId = item.ProductId,
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    Brand = item.Product.Brand,
                    PictureUrl = item.Product.PictureUrl,
                    Quantity = item.Product.QuantityInStock,
                    Type = item.Product.Type,
                }).ToList()
            };
        }
    }
}
