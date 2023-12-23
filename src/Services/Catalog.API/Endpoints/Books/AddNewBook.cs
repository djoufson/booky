using Catalog.API.Infra.Data;
using Catalog.API.Models;
using Catalog.API.Models.Types;
using Catalog.API.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Catalog.Dtos;
using Shared.Contracts.Catalog.Requests;

namespace Catalog.API.Endpoints.Books;

public partial class CatalogEndpoints
{
    public static async Task<Results<Ok<BookDto>,NotFound>> AddNewBook(
        [FromBody] AddBookRequest request,
        CatalogDbContext context,
        ImageService imageService)
    {
        // Get the author
        var authorId = new AuthorId(request.AuthorId);
        var author = await context.Authors.FindAsync(authorId);
        if(author is null)
            return TypedResults.NotFound();

        var slug = GenerateSlug(request.Title);

        // Create and save the image
        var imageUrl = await imageService.Save(request.ImageBase64, slug);

        // Create the book entity
        var book = Book.Create(
            request.Title,
            request.Description,
            request.Price,
            null,
            author,
            imageUrl,
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
        var bookEntity = (await context.AddAsync(book)).Entity;
        await context.SaveChangesAsync();

        var bookDto = new BookDto(
            bookEntity.Id.Value.ToString(),
            bookEntity.Title,
            bookEntity.Description,
            bookEntity.Price,
            bookEntity.OldPrice,
            new AuthorDto(
                bookEntity.AuthorId.Value.ToString(),
                bookEntity.Author.UserName.Value,
                bookEntity.Author.Name.First,
                bookEntity.Author.Name.Last,
                bookEntity.Author.Email.Value,
                bookEntity.Author.Bio,
                bookEntity.Author.ImageUrl
            ),
            bookEntity.CreatedAt,
            bookEntity.UpdatedAt,
            bookEntity.Tags.Select(b => b.Tag).ToArray()
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
