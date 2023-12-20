using Catalog.API.Endpoints;

namespace Catalog.API.Extension;

internal static class Endpoints
{
    public static IEndpointRouteBuilder MapCatalogEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", CatalogEndpoints.GetAllBooks);
        app.MapGet("/search", CatalogEndpoints.SearchBooks);
        app.MapGet("/{id:Guid}", CatalogEndpoints.GetSingleBook);

        app.MapPost("/", CatalogEndpoints.AddNewBook);
        app.MapDelete("/{id:Guid}", CatalogEndpoints.DeleteBook);
        app.MapPut("/{id:Guid}", CatalogEndpoints.EditBookInfos);
        app.MapPatch("/{id:Guid}", CatalogEndpoints.EditBookInfos);
        return app;
    }
}
