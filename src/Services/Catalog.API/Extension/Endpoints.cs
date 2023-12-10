using Catalog.API.Endpoints;

namespace Catalog.API.Extension;

internal static class Endpoints
{
    public static IEndpointRouteBuilder MapCatalogEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", CatalogEndpoints.GetAllBooks);
        app.MapPost("/", CatalogEndpoints.AddNewBook);
        app.MapDelete("/{id:Guid}", CatalogEndpoints.DeleteBook);
        return app;
    }
}
