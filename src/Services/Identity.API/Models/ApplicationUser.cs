using Microsoft.AspNetCore.Identity;

namespace Identity.API.Models;

public sealed class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
}
