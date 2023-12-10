namespace Catalog.API.Contracts.Requests;

public record EditBookInfosRequest(
    string Title,
    string Description,
    decimal Price,
    decimal? OldPrice
);
