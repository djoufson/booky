using Catalog.API.Contracts.Dtos;
using Catalog.API.Infra.Data;
using Catalog.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Endpoints;

public partial class CatalogEndpoints
{
    // https://learn.microsoft.com/ef/core/performance/advanced-performance-topics#compiled-queries
    private static readonly Func<CatalogDbContext, IAsyncEnumerable<Book>> s_getCatalogQuery =
        EF.CompileAsyncQuery((CatalogDbContext ctx) => ctx.Books.AsNoTracking());

    public static async Task<Results<Ok<BookDto[]>, BadRequest>> GetAllBooks(CatalogDbContext context)
    {
        var books = await ToListAsync(s_getCatalogQuery(context));

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

    private static async Task<List<T>> ToListAsync<T>(IAsyncEnumerable<T> asyncEnumerable)
    {
        var results = new List<T>();
        await foreach (var item in asyncEnumerable)
        {
            results.Add(item);
        }

        return results;
    }
}
