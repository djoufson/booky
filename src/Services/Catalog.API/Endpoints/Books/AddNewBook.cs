using Catalog.API.Infra.Data;
using Catalog.API.Models;
using Catalog.API.Models.Types;
using Catalog.API.Options;
using Catalog.API.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Contracts.Catalog.Dtos;
using Shared.Contracts.Catalog.Requests;

namespace Catalog.API.Endpoints.Books;

public partial class CatalogEndpoints
{
    public static async Task<Results<Ok<BookDto>,NotFound>> AddNewBook(
        [FromBody] AddBookRequest request,
        CatalogDbContext context,
        ImageService imageService,
        IOptions<CatalogOptions> options)
    {
        var opt = options.Value;
        // Get the author
        var authorId = new AuthorId(request.AuthorId);
        var author = await context.Authors.FindAsync(authorId);
        if(author is null)
            return TypedResults.NotFound();

        var slug = GenerateSlug(request.Title);

        // Create and save the image
        var coverImage = await imageService.Save(request.ImageBase64, slug);

        // Create the book entity
        var book = Book.Create(
            request.Title,
            slug,
            request.Description,
            request.Price,
            null,
            author,
            coverImage,
            DateTime.UtcNow,
            DateTime.UtcNow
        );

        // Get/Create the tags
        foreach (var tagItem in request.Tags)
        {
            var tag = await context.Tags.FirstOrDefaultAsync(t => t.Tag == tagItem) ?? new BookTag(tagItem);
            book.Tag(tag);
        }

        // Save to the database
        book = (await context.AddAsync(book)).Entity;
        await context.SaveChangesAsync();

        var bookDto = new BookDto(
            book.Id.Value.ToString(),
            book.Title,
            book.Slug,
            book.Description,
            book.Price,
            book.OldPrice,
            new AuthorDto(
                book.AuthorId.Value.ToString(),
                book.Author.UserName.Value,
                book.Author.Name.First,
                book.Author.Name.Last,
                book.Author.Email.Value,
                book.Author.Bio,
                opt.PicBaseUrl.Replace("[0]", book.Author.ImageUrl)
            ),
            opt.PicBaseUrl.Replace("[0]", book.Id.Value.ToString()),
            book.CreatedAt,
            book.UpdatedAt,
            book.Tags.Select(b => b.Tag).ToArray()
        );
        return TypedResults.Ok(bookDto);
    }

    private static string GenerateSlug(string title)
    {
        return title
            .ToLower()
            .Split(" ")
            .Aggregate((crr, acc) => crr += $"-{acc}");
    }
}
