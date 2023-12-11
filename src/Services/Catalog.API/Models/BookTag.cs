namespace Catalog.API.Models;

internal record BookTag(string Tag)
{
    public int Id { get; private set; }
    public ICollection<Book> Books { get; set; } = [];
}
