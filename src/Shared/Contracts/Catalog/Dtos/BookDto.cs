namespace Shared.Contracts.Catalog.Dtos;

public record BookDto(
    string Id,
    string Title,
    string Slug,
    string Description,
    decimal Price,
    decimal? OldPrice,
    AuthorDto Author,
    string? CoverImage,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    string[] Tags
);
