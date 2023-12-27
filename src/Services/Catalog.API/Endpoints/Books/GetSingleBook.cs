using Catalog.API.Infra.Data;
using Catalog.API.Models.Types;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Catalog.Dtos;

namespace Catalog.API.Endpoints.Books;

public partial class CatalogEndpoints
{
    public static async Task<Results<Ok<BookDto>, NotFound>> GetSingleBook(Guid id, CatalogDbContext context)
    {
        var bookId = new BookId(id);
        var book = await context.Books
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Id == bookId);

        if(book is null)
            return TypedResults.NotFound();

        var bookDto = new BookDto(
            book.Id.Value.ToString(),
            book.Title,
            book.Description,
            book.Price,
            book.OldPrice,
            new AuthorDto(
                book.AuthorId.Value.ToString(),
                book.Author.UserName.Value,
                book.Author.Name.First,
                book.Author.Name.Last,
                book.Author.Email.Value,
                book.Author.Bio,
                book.Author.ImageUrl
            ),
            book.CoverImage,
            book.CreatedAt,
            book.UpdatedAt,
            book.Tags.Select(t => t.Tag).ToArray()
        );
        return TypedResults.Ok(bookDto);
    }
}
