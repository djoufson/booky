using Catalog.API.Extensions;
using Catalog.API.Infra.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Catalog.Dtos;

namespace Catalog.API.Endpoints.Books;

public partial class CatalogEndpoints
{
    public static async Task<Results<Ok<BookDto[]>, NotFound>> SearchBooks(
        [FromServices] CatalogDbContext context,
        [FromQuery] string searchQuery,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20)
    {
        var books = await CatalogDbContext.SearchByNameOrTagOrAuthor(context, searchQuery, pageNumber, pageSize).ToListAsync();
        var response = books.Select(b => new BookDto(
            b.Id.Value.ToString(),
            b.Title,
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
                b.Author.ImageUrl
            ),
            b.CoverImage,
            b.CreatedAt,
            b.UpdatedAt,
            b.Tags.Select(t => t.Tag).ToArray()
        )).ToArray();
        return TypedResults.Ok(response);
    }
}
