using Catalog.API.Endpoints;

namespace Catalog.API.Extension;

internal static class Endpoints
{
    public static IEndpointRouteBuilder MapCatalogEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", CatalogEndpoints.GetAllBooks);
        return app;
    }
}
