using System.Security.Claims;
using Identity.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Shared.Contracts.Identity.Dtos;

namespace Identity.API.Apis.Endpoints;

public partial class IdentityEndpoints
{
    public static async Task<Results<Ok<UserDto>,UnauthorizedHttpResult>> GetState(
        UserManager<ApplicationUser> userManager,
        HttpContext context
    )
    {
        if (!context.User?.Identity?.IsAuthenticated ?? true)
            return TypedResults.Unauthorized();

        string userId = context.User!.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var user = await userManager.FindByIdAsync(userId);
        if(user is null)
            return TypedResults.Unauthorized();

        return TypedResults.Ok(new UserDto(
            user.Id,
            user.UserName!,
            user.FirstName,
            user.LastName,
            user.Email!,
            user.Role
        ));
    }
}
