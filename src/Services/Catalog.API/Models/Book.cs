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
        DateTime updatedAt)
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
    }

    public static Book Create(
        string title,
        string description,
        decimal price,
        decimal? oldPrice,
        Author author,
        DateTime createdAt,
        DateTime updatedAt)
    {
        return new Book(
            BookId.Create(),
            title,
            description,
            price,
            oldPrice,
            author,
            createdAt,
            updatedAt);
    }
}
