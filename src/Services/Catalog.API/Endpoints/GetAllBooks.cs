using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.API.Endpoints;

public partial class CatalogEndpoints
{
    public static Results<Ok, BadRequest> GetAllBooks()
    {
        return TypedResults.Ok();
    }
}
