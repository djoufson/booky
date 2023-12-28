using Catalog.API.Infra.Data;
using Catalog.API.Models.Types;
using Catalog.API.Options;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.Contracts.Catalog.Dtos;
using Shared.Contracts.Catalog.Requests;

namespace Catalog.API.Endpoints.Books;

public partial class CatalogEndpoints
{
    public static async Task<Results<Ok<BookDto>,NotFound>> EditBookInfos(
        [FromRoute] Guid id,
        [FromBody] EditBookInfosRequest request,
        CatalogDbContext context,
        IOptions<CatalogOptions> options
    )
    {
        var opt = options.Value;
        // Get the book from the Database
        var bookId = new BookId(id);
        var book = await context.Books.FindAsync(bookId);
        if(book is null)
            return TypedResults.NotFound();

        // Update the book
        bool updated = book.UpdateInfos(
            request.Title,
            request.Description,
            request.Price,
            request.OldPrice);

        if(updated)
            context.Update(book);

        await context.SaveChangesAsync();

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
