namespace Basket.API.Models;

internal class Book
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string ExternalId { get; private set; }
    public string Title { get; private set; }
    public string Slug { get; private set; }
    public decimal Price { get; private set; }
    public decimal? OldPrice { get; private set; }
    public string Author { get; private set; }
    public string? CoverImage { get; private set; }
    public bool Deleted { get; private set; }

    public Book(string externalId, string title, string slug, decimal price, decimal? oldPrice, string author, string? coverImage)
    {
        ExternalId = externalId;
        Title = title;
        Slug = slug;
        Price = price;
        OldPrice = oldPrice;
        Author = author;
        CoverImage = coverImage;
    }

    #pragma warning disable CS8618
    private Book()
    {
    }
    #pragma warning restore CS8618
}
