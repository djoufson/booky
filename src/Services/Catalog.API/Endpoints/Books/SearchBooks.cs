using Catalog.API.Extensions;
using Catalog.API.Infra.Data;
using Catalog.API.Models;
using Catalog.API.Options;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Contracts.Catalog.Dtos;

namespace Catalog.API.Endpoints.Books;

public partial class CatalogEndpoints
{
    public static async Task<Results<Ok<BookDto[]>, NotFound>> SearchBooks(
        [FromServices] CatalogDbContext context,
        [FromServices] IOptions<CatalogOptions> options,
        [FromQuery] string search,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20)
    {
        var opt = options.Value;
        Book[] books = [];
        if(!string.IsNullOrWhiteSpace(search))
        {
            books = await CatalogDbContext.SearchByNameOrTagOrAuthor(context, search, pageNumber, pageSize).ToArrayAsync();
        }
        else
        {
            books = await context.Books.ToArrayAsync();
        }

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
