using Microsoft.AspNetCore.Identity;

namespace Identity.API.Models;

public sealed class ApplicationUser : IdentityUser
{
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
}
