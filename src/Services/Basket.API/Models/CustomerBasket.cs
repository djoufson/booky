namespace Basket.API.Models;

internal class CustomerBasket
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public ICollection<BasketItem> Items { get; private set; } = [];
}
