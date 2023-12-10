using Catalog.API.Contracts.Dtos;
using Catalog.API.Infra.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Endpoints;

public partial class CatalogEndpoints
{
    public static async Task<Results<Ok<BookDto[]>, BadRequest>> GetAllBooks(CatalogDbContext context)
    {
        var books = await context.Books.ToArrayAsync();
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
            b.CreatedAt,
            b.UpdatedAt
        )).ToArray();
        return TypedResults.Ok(response);
    }
}
