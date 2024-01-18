namespace Basket.API.Models;

internal class BasketItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid BookId { get; private set; }
    public string BookTitle { get; private set; }
    public string BookSlug { get; private set; }
    public decimal BookPrice { get; private set; }
    public decimal? BookOldPrice { get; private set; }
    public string BookAuthor { get; private set; }
    public string? BookCoverImage { get; private set; }
    public DateTime AddedOn { get; private set; }
    public int Quantity { get; private set; }

    public BasketItem(Guid id, Guid bookId, string bookTitle, string bookSlug, decimal bookPrice, decimal? bookOldPrice, string bookAuthor, string? bookCoverImage, DateTime addedOn)
    {
        Id = id;
        BookId = bookId;
        BookTitle = bookTitle;
        BookSlug = bookSlug;
        BookPrice = bookPrice;
        BookOldPrice = bookOldPrice;
        BookAuthor = bookAuthor;
        BookCoverImage = bookCoverImage;
        AddedOn = addedOn;
    }

#pragma warning disable CS8618
    private BasketItem()
    {
    }
#pragma warning restore CS8618
}
