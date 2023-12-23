namespace Shared.Contracts.Catalog.Requests;

public record EditBookInfosRequest(
    string Title,
    string Description,
    decimal Price,
    decimal? OldPrice
);
