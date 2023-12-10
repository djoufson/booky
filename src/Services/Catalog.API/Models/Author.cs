using Catalog.API.Models.Types;

namespace Catalog.API.Models;

internal class Author
{
    private readonly List<Book> _books = [];
    public AuthorId Id { get; set; }
    public UserName UserName { get; set; }
    public Name Name { get; set; }
    public Email Email { get; set; }
    public string Bio { get; set; }
    public string? ImageUrl { get; set; }
    public IReadOnlyList<Book> Books => _books.ToArray();

    private Author(
        AuthorId id,
        UserName userName,
        Name name,
        Email email,
        string bio,
        string? imageUrl)
    {
        Id = id;
        UserName = userName;
        Name = name;
        Email = email;
        Bio = bio;
        ImageUrl = imageUrl;
    }

    public static Author Create(
        UserName userName,
        Name name,
        Email email,
        string bio,
        string? imageUrl)
    {
        return new(
            AuthorId.Create(),
            userName,
            name,
            email,
            bio,
            imageUrl);
    }
}
