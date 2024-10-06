namespace Services.API.Entities
{
    public class Basket
    {
        public Guid Id { get; set; }

        public Guid BuyerId { get; set; }

        public List<BasketItem> Items { get; set; } = new();

        public void AddItem(Product product, int quantity)
        {
            if (Items.All(item => item.ProductId != product.Id))
            {
                Items.Add(new BasketItem { Product = product, Quantity = quantity });
            }

            var excistingItem = Items.FirstOrDefault(item => item.ProductId == product.Id);

            if (excistingItem != null)
            {
                excistingItem.Quantity += quantity;
            }
        }

        public void RemoveItem(Guid productId, int quantity)
        {
            var item = Items.FirstOrDefault(item => item.ProductId == productId);

            if (item != null)
            {
                item.Quantity -= quantity;

                if (item.Quantity <= 0)
                {
                    Items.Remove(item);
                }
            }
        }
    }
}
