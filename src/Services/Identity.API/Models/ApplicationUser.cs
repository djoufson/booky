using Identity.API.Utilities;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Models;

public sealed class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public string Role { get; set; } = Constants.Roles.USER;
}
