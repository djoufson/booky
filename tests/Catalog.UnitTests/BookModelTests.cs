using Catalog.API.Models;
using Catalog.API.Models.Types;

namespace Catalog.UnitTests;

public class BookModelTests
{
    [Fact]
    public void TagMethod_ShouldAddNewTag()
    {
        // Arrange
        var author = Author.Create(new UserName("author"), new Name("John", "Doe"), new Email("john@email.com"), "Short description", string.Empty);
        var book = Book.Create("Book Title", "Book Description", 200, null, author, DateTime.Now, DateTime.Now);
        var horrorTag = new BookTag("horror");
        var romanceTag = new BookTag("romance");
        var theatreTag = new BookTag("theatre");

        // Act
        book.Tag(horrorTag);
        book.Tag(horrorTag);
        book.Tag(romanceTag);
        book.Tag(theatreTag);

        // Assert
        Assert.NotEmpty(book.Tags);
        Assert.Equal(3, book.Tags.Count);
    }

    [Fact]
    public void UntagMethod_ShouldRemoveTheSpecifiesTag()
    {
        // Arrange
        var author = Author.Create(new UserName("author"), new Name("John", "Doe"), new Email("john@email.com"), "Short description", string.Empty);
        var horrorTag = new BookTag("horror");
        var romanceTag = new BookTag("romance");
        var theatreTag = new BookTag("theatre");
        var book = Book.Create("Book Title", "Book Description", 200, null, author, DateTime.Now, DateTime.Now, horrorTag, romanceTag, theatreTag);

        // Act
        bool act1 = book.UnTag(horrorTag);
        bool act2 = book.UnTag(horrorTag);
        bool act3 = book.UnTag(romanceTag);
        bool act4 = book.UnTag(theatreTag);

        // Assert
        Assert.True(act1);
        Assert.False(act2);
        Assert.True(act3);
        Assert.True(act4);
        Assert.Equal(0, book.Tags.Count);
    }

    [Theory]
    [InlineData("", "", 0, 0, false)]
    [InlineData(" ", "", 0, 0, false)]
    [InlineData(" ", " ", 0, 0, false)]
    [InlineData("", " ", 0, 0, false)]
    [InlineData("", " ", -20, 0, false)]
    [InlineData("", " ", -20, -10, false)]
    [InlineData("Book Title", "", -20, -10, false)]
    [InlineData("Book Title", "Book Description", 20, 0, true)]
    [InlineData("", "Book Descriptions", 0, 0, true)]
    [InlineData("Book", "Book Description", 0, 0, true)]
    [InlineData("Book Title", "Book Description", 0, 20, true)]
    public void UpdateInfos_ShouldReturnTheUpdateStatus(string title, string description, decimal price, decimal oldPrice, bool expected)
    {
        // Arrange
        var author = Author.Create(new UserName("author"), new Name("John", "Doe"), new Email("john@email.com"), "Short description", string.Empty);
        var book = Book.Create("Book Title", "Book Description", 200, null, author, DateTime.Now, DateTime.Now);

        // Act
        bool act1 = book.UpdateInfos();
        bool act2 = book.UpdateInfos(
            title: title,
            description: description,
            price: price,
            oldPrice: oldPrice);

        // Assert
        Assert.False(act1);
        Assert.Equal(expected, act2);
    }

    [Fact]
    public void UpdateInfos_ShouldEffectivelyUpdateBookInfos()
    {
        // Arrange
        var author = Author.Create(new UserName("author"), new Name("John", "Doe"), new Email("john@email.com"), "Short description", string.Empty);
        var book = Book.Create("Book Title", "Book Description", 200, null, author, DateTime.Now, DateTime.Now);

        // Act
        book.UpdateInfos(
            title: "New Title",
            description: "New Description",
            price: 300,
            oldPrice: 400);

        // Assert
        Assert.Equal("New Title", book.Title);
        Assert.Equal("New Description", book.Description);
        Assert.Equal(300, book.Price);
        Assert.Equal(400, book.OldPrice);
    }
}
