using Catalog.API.Infra.Data;
using Catalog.API.Models.Types;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Endpoints.Books;

public partial class CatalogEndpoints
{
    public static async Task<Results<Ok,BadRequest>> DeleteBook(
        [FromRoute] Guid id,
        CatalogDbContext context
    )
    {
        var bookId = new BookId(id);
        await context.Books
            .Where(b => b.Id == bookId)
            .ExecuteDeleteAsync();

        return TypedResults.Ok();
    }
}
