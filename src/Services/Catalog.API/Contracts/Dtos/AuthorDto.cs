namespace Catalog.API.Contracts.Dtos;

public record AuthorDto(
    string Id,
    string UserName,
    string FirstName,
    string? LastName,
    string Email,
    string Bio,
    string? ImageUrl
);
