namespace Basket.API.Models;

internal class Customer
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string ExternalId { get; private set; }
    public string Username { get; private set; }
    public CustomerBasket? Basket { get; private set; }

    public Customer(string externalId, string username)
    {
        ExternalId = externalId;
        Username = username;
        Basket = CustomerBasket.Empty(this);
    }

    public Customer(string externalId, string username, CustomerBasket basket)
    {
        ExternalId = externalId;
        Username = username;
        Basket = basket;
    }

#pragma warning disable CS8618
    private Customer()
    {
    }
#pragma warning restore CS8618
}
