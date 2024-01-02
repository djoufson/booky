namespace Shared.Contracts.Identity.Dtos;

public record UserDto(
    string Id,
    string UserName,
    string FirstName,
    string? LastName,
    string Email,
    string Role
);