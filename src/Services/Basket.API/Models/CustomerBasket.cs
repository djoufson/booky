namespace Basket.API.Models;

internal class CustomerBasket
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public ICollection<BasketItem> Items { get; private set; } = [];
    public Customer Customer { get; private set; }
    public Guid CustomerId { get; private set; }

    public CustomerBasket(Guid id, Customer customer)
    {
        Id = id;
        Customer = customer;
        CustomerId = customer.Id;
    }

#pragma warning disable CS8618
    private CustomerBasket()
    {
    }
#pragma warning restore CS8618

    public static CustomerBasket Empty(Customer customer)
    {
        return new(Guid.NewGuid(), customer);
    }
}
