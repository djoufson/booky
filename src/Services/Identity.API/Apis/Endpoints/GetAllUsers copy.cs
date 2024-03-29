using System.Security.Claims;
using Identity.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Apis.Endpoints;

public partial class IdentityEndpoints
{
    public static async Task<Results<Ok, NotFound, UnauthorizedHttpResult>> UpgradeToAuthor(
        HttpContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationUser> roleManager
    )
    {
        // Get the user
        var userId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId is null)
            return TypedResults.Unauthorized();
        var user = await userManager.FindByIdAsync(userId);
        if (user is null)
            return TypedResults.NotFound();

        // Assign to a role
        if (!await roleManager.RoleExistsAsync(Roles.Author))
        {
            await roleManager.CreateAsync(user);
        }

        // Return
        return TypedResults.Ok();
    }
}
