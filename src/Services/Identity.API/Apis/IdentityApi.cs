using Identity.API.Apis.Endpoints;

namespace Identity.API.Apis;

public static class IdentityApi
{
    public static IEndpointRouteBuilder MapIdentityEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("login", IdentityEndpoints.Login);

        // app.MapPut("upgrade", UpgradeToAuthor)
        //     .RequireAuthorization();

        // app.MapPut("upgrade/pro", () => "");
        return app;
    }

    // private static async Task<Results<Ok, NotFound, UnauthorizedHttpResult>> UpgradeToAuthor(
    //     HttpContext context,
    //     UserManager<ApplicationUser> userManager,
    //     RoleManager<ApplicationUser> roleManager
    // )
    // {
    //     // Get the user
    //     var userId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    //     if (userId is null)
    //         return TypedResults.Unauthorized();
    //     var user = await userManager.FindByIdAsync(userId);
    //     if (user is null)
    //         return TypedResults.NotFound();

    //     // Assign to a role
    //     if (!await roleManager.RoleExistsAsync(Roles.Author))
    //     {
    //         await roleManager.CreateAsync(user);
    //     }

    //     // Return
    //     return TypedResults.Ok();
    // }
}
