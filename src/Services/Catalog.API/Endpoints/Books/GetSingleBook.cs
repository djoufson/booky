using Catalog.API.Infra.Data;
using Catalog.API.Models.Types;
using Catalog.API.Options;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Contracts.Catalog.Dtos;

namespace Catalog.API.Endpoints.Books;

public partial class CatalogEndpoints
{
    public static async Task<Results<Ok<BookDto>, NotFound>> GetSingleBookById(
        Guid id,
        CatalogDbContext context,
        IOptions<CatalogOptions> options)
    {
        var opt = options.Value;
        var bookId = new BookId(id);
        var book = await context.Books
            .FirstOrDefaultAsync(b => b.Id == bookId);

        if(book is null)
            return TypedResults.NotFound();

        var bookDto = new BookDto(
            book.Id.Value.ToString(),
            book.Title,
            book.Slug,
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
                opt.PicBaseUrl.Replace("[0]", book.Author.ImageUrl)
            ),
            opt.PicBaseUrl.Replace("[0]", book.Id.Value.ToString()),
            book.CreatedAt,
            book.UpdatedAt,
            book.Tags.Select(t => t.Tag).ToArray()
        );
        return TypedResults.Ok(bookDto);
    }
}
