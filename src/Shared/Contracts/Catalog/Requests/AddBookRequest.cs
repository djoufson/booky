namespace Shared.Contracts.Catalog.Requests;

public record AddBookRequest(
    Guid AuthorId,
    string Title,
    string Description,
    decimal Price,
    string ImageBase64,
    string[] Tags
);
