using Identity.API.Data;
using Identity.API.Models;
using Identity.API.Services.Abstraction;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Apis.Endpoints;

public partial class IdentityEndpoints
{
    public static async Task<Results<Ok,Ok<JwtResult>,UnauthorizedHttpResult>> Login(
        [FromBody] LoginRequest request,
        [FromServices] UserManager<ApplicationUser> userManager,
        [FromServices] SignInManager<ApplicationUser> signInManager,
        [FromServices] IJwtTokenGenerator jwtTokenGenerator,
        [FromServices] ILogger<IdentityEndpoints> logger,
        [FromServices] ApplicationDbContext dbContext,
        [FromQuery] bool cookie = true
    )
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if(user is null)
        {
            logger.LogInformation("Unable to find the user");
            return TypedResults.Unauthorized();
        }

        if(await userManager.CheckPasswordAsync(user, request.Password))
        {
            logger.LogInformation("Password mismatch");
            return TypedResults.Unauthorized();
        }

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
