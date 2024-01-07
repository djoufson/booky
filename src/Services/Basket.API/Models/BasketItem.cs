namespace Basket.API.Models;

internal class BasketItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Book Book { get; private set; }
    public Guid BookId { get; private set; }
    public DateTime AddedOn { get; private set; }

    public BasketItem(Book book, Guid bookId, DateTime addedOn)
    {
        Book = book;
        BookId = bookId;
        AddedOn = addedOn;
    }

    #pragma warning disable CS8618
    private BasketItem()
    {
    }
    #pragma warning restore CS8618
}
