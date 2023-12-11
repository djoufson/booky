using Catalog.API.Models.Types;

namespace Catalog.API.Models;

internal class Book
{
    private readonly List<BookTag> _tags = [];
    public BookId Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public decimal? OldPrice { get; private set; } // If there is a sold
    public Author Author { get; set; }
    public AuthorId AuthorId { get; set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public IReadOnlyList<BookTag> Tags => _tags.ToArray();

    private Book(
        BookId id,
        string title,
        string description,
        decimal price,
        decimal? oldPrice,
        Author author,
        DateTime createdAt,
        DateTime updatedAt,
        BookTag[] tags)
    {
        Id = id;
        Title = title;
        Description = description;
        Price = price;
        OldPrice = oldPrice;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Author = author;
        AuthorId = author.Id;
        _tags = [.. tags];
    }

    #pragma warning disable CS8618
    private Book()
    {
    }
    #pragma warning restore

    public static Book Create(
        string title,
        string description,
        decimal price,
        decimal? oldPrice,
        Author author,
        DateTime createdAt,
        DateTime updatedAt,
        params BookTag[] tags)
    {
        return new Book(
            BookId.Create(),
            title,
            description,
            price,
            oldPrice,
            author,
            createdAt,
            updatedAt,
            tags);
    }

    public void Tag(BookTag tag)
    {
        if(_tags.Contains(tag))
            return;
        _tags.Add(tag);
    }

    public bool UnTag(BookTag tag)
    {
        return _tags.Remove(tag);
    }

    internal bool UpdateInfos(string title = "", string description = "", decimal price = 0, decimal? oldPrice = 0)
    {
        bool updated = false;
        if(!string.IsNullOrWhiteSpace(title) && !Title.Equals(title))
        {
            Title = title;
            updated = true;
        }

        if(!string.IsNullOrWhiteSpace(description) && !Description.Equals(description))
        {
            Description = description;
            updated = true;
        }

        if(price > 0 && price != Price)
        {
            Price = price;
            updated = true;
        }

        if(oldPrice > 0 && oldPrice != OldPrice)
        {
            OldPrice = oldPrice;
            updated = true;
        }

        return updated;
    }
}
