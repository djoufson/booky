using Catalog.API.Infra.Data;
using MassTransit.Initializers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Catalog.Dtos;

namespace Catalog.API.Endpoints.Books;

public partial class TagEndpoints
{
    public static async Task<Results<Ok<TagDto[]>,BadRequest>> GetAllTags(
        [FromServices] CatalogDbContext dbContext
    )
    {
        var tags = await dbContext.Tags.ToArrayAsync();
        var tagsDto = tags.Select(t => new TagDto(t.Tag)).ToArray();
        return TypedResults.Ok(tagsDto);
    }
}
