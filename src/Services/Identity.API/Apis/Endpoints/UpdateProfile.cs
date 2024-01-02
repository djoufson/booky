using Identity.API.Extensions;
using Identity.API.Models;
using Identity.API.Utilities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Apis.Endpoints;

public partial class IdentityEndpoints()
{
    public static async Task<Results<Ok, UnauthorizedHttpResult>> UpdateProfile(
        [FromBody] UpdateProfileRequest request,
        [FromServices] ILogger<IdentityEndpoints> logger,
        [FromServices] UserManager<ApplicationUser> userManager,
        HttpContext context
    )
    {
        var userId = context.Request.Headers.GetValue(Constants.HeadersKeys.USER_ID);
        logger.LogInformation("User Id: {UserId}", userId);
        var user = await userManager.FindByIdAsync(userId);
        if(user is null)
        {
            return TypedResults.Unauthorized();
        }

        user.UserName = request.UserName;
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        await userManager.UpdateAsync(user);
        return TypedResults.Ok();
    }

    public record UpdateProfileRequest(
        string UserName,
        string FirstName,
        string? LastName
    );
}
