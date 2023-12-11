namespace Catalog.API.Contracts.Dtos;

public record BookDto(
    string Id,
    string Title,
    string Description,
    decimal Price,
    decimal? OldPrice,
    AuthorDto Author,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    string[] Tags
);
