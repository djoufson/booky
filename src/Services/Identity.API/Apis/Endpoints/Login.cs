using Identity.API.Models;
using Identity.API.Services.Abstraction;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Apis.Endpoints;

public partial class IdentityEndpoints
{
    public static async Task<Results<Ok,Ok<JwtResult>,UnauthorizedHttpResult>> Login(
        [FromBody] LoginRequest request,
        [FromServices] UserManager<ApplicationUser> userManager,
        [FromServices] SignInManager<ApplicationUser> signInManager,
        [FromServices] IJwtTokenGenerator jwtTokenGenerator,
        [FromQuery] bool cookie = true
    )
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if(user is null)
            return TypedResults.Unauthorized();

        if(await userManager.CheckPasswordAsync(user, request.Password))
            return TypedResults.Unauthorized();

        if(cookie)
        {
            await signInManager.SignInAsync(user, true);
            return TypedResults.Ok();
        }

        var token = await jwtTokenGenerator.GenerateAsync(user);
        return TypedResults.Ok(token);
    }

    public record LoginRequest(
        string Email,
        string Password
    );
}
