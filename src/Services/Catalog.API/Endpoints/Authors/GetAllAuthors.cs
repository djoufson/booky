using Catalog.API.Infra.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Catalog.Dtos;

namespace Catalog.API.Endpoints.Authors;

public partial class AuthorsEndpoints
{
    public static async Task<Results<Ok<AuthorDto[]>, BadRequest>> GetAllAuthors(CatalogDbContext context)
    {
        var authors = await context.Authors.ToArrayAsync();
        var authorDtos = authors.Select(a =>
        {
            return new AuthorDto
            (
                a.Id.Value.ToString(),
                a.UserName.Value,
                a.Name.First,
                a.Name.Last,
                a.Email.Value,
                a.Bio,
                a.ImageUrl
            );
        }).ToArray();
        return TypedResults.Ok(authorDtos);
    }
}
