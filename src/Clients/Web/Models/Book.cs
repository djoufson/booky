namespace Web.Models;

public record Book(
    string Id,
    string Title,
    string Description,
    decimal Price,
    decimal? OldPrice,
    Author Author,
    string? CoverImage,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    string[] Tags
);
