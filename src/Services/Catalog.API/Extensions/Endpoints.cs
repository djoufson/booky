using Catalog.API.Caching;
using Catalog.API.Endpoints.Authors;
using Catalog.API.Endpoints.Books;

namespace Catalog.API.Extensions;

internal static class Endpoints
{
    public static IEndpointRouteBuilder MapCatalogEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", CatalogEndpoints.GetBooks).CacheOutput(Cache.Policies.GetBooks);
        app.MapGet("/{id:Guid}", CatalogEndpoints.GetSingleBookById);
        app.MapGet("/{slug}", CatalogEndpoints.GetSingleBookBySlug);
        app.MapPost("/", CatalogEndpoints.AddNewBook);
        app.MapDelete("/{id:Guid}", CatalogEndpoints.DeleteBook);
        app.MapPut("/{id:Guid}", CatalogEndpoints.EditBookInfos);
        app.MapPatch("/{id:Guid}", CatalogEndpoints.EditBookInfos);
        app.MapGet("/{id:Guid}/image", CatalogEndpoints.GetBookCover);
        return app;
    }

    public static IEndpointRouteBuilder MapAuthorsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", AuthorsEndpoints.GetAllAuthors).CacheOutput(Cache.Policies.Authors);
        return app;
    }

    public static IEndpointRouteBuilder MapTagsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", TagEndpoints.GetAllTags).CacheOutput(Cache.Policies.GetAllTags);
        return app;
    }
}
