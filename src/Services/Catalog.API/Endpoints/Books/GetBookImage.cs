using Catalog.API.Infra.Data;
using Catalog.API.Models.Types;
using Catalog.API.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.API.Endpoints.Books;

public partial class CatalogEndpoints
{
    public static async Task<Results<PhysicalFileHttpResult,NotFound>> GetBookCover(
        Guid id,
        CatalogDbContext context,
        ImageService imageService,
        CancellationToken ct)
    {
        var bookId = new BookId(id);
        var book = await context.Books.FindAsync([bookId], ct);
        if(book is null)
        {
            return TypedResults.NotFound();
        }

        if(book.CoverImage is null)
        {
            return TypedResults.NotFound();
        }

        var (path, mimeType) = imageService.Retrieve(book.CoverImage);
        return TypedResults.PhysicalFile(path, mimeType);
    }
}
