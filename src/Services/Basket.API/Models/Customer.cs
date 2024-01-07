namespace Basket.API.Models;

internal class Customer
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string ExternalId { get; private set; }
    public string Username { get; private set; }
    public CustomerBasket Basket { get; private set; } = new();

    public Customer(string externalId, string username)
    {
        ExternalId = externalId;
        Username = username;
    }

    #pragma warning disable CS8618
    private Customer()
    {
    }
    #pragma warning restore CS8618
}
