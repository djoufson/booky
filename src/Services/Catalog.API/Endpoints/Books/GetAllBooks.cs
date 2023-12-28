using Catalog.API.Extensions;
using Catalog.API.Infra.Data;
using Catalog.API.Options;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Contracts.Catalog.Dtos;

namespace Catalog.API.Endpoints.Books;

public partial class CatalogEndpoints
{
    public static async Task<Results<Ok<BookDto[]>, BadRequest>> GetAllBooks(
        CatalogDbContext context,
        IOptions<CatalogOptions> options,
        int pageNumber = 1,
        int pageSize = 20)
    {
        var books = await CatalogDbContext.GetAllBooksQuery(context, pageNumber, pageSize).ToListAsync();
        var opt = options.Value;

        var response = books.Select(b => new BookDto(
            b.Id.Value.ToString(),
            b.Title,
            b.Slug,
            b.Description,
            b.Price,
            b.OldPrice,
            new AuthorDto(
                b.AuthorId.Value.ToString(),
                b.Author.UserName.Value,
                b.Author.Name.First,
                b.Author.Name.Last,
                b.Author.Email.Value,
                b.Author.Bio,
                opt.PicBaseUrl.Replace("[0]", b.Author.ImageUrl)
            ),
            opt.PicBaseUrl.Replace("[0]", b.Id.Value.ToString()),
            b.CreatedAt,
            b.UpdatedAt,
            b.Tags.Select(t => t.Tag).ToArray()
        )).ToArray();
        return TypedResults.Ok(response);
    }
}
