
namespace Catalog.API.Contracts.Requests;

public record AddBookRequest(
    Guid AuthorId,
    string Title,
    string Description,
    decimal Price,
    string ImageBase64,
    string[] Tags
);
