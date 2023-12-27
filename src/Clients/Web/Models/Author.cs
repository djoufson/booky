namespace Web.Models;

public record Author(
    string Id,
    string UserName,
    string FirstName,
    string? LastName,
    string Email,
    string Bio,
    string? ImageUrl
);