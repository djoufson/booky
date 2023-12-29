using System.Security.Claims;
using Identity.API.Data;
using Identity.API.Models;
using Identity.API.Services.Abstraction;
using Microsoft.AspNetCore.Authentication;
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
        [FromServices] IJwtTokenGenerator jwtTokenGenerator,
        [FromServices] ILogger<IdentityEndpoints> logger,
        [FromServices] ApplicationDbContext dbContext,
        HttpContext httpContext,
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
            var claims = await GetClaims(user, userManager);
            var claimsIdentity = new ClaimsIdentity(claims, "server-auth");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await httpContext.SignInAsync(claimsPrincipal);

            logger.LogInformation("Successfully logged in using Cookie");
            return TypedResults.Ok();
        }

        var token = await jwtTokenGenerator.GenerateAsync(user);
        logger.LogInformation("Successfully logged in using JWT");
        return TypedResults.Ok(token);
    }

    private static async Task<Claim[]> GetClaims(ApplicationUser user, UserManager<ApplicationUser> userManager)
    {
        var claims = new List<Claim>()
        {
            new (ClaimTypes.NameIdentifier, user.Id),
            new (ClaimTypes.Name, user.UserName!)
        };
        var roles = await userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
        return [.. claims];
    }

    public record LoginRequest(
        string Email,
        string Password
    );
}
