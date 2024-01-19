using System.Security.Claims;
using Identity.API.Data;
using Identity.API.Models;
using Identity.API.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Apis.Endpoints;

public partial class IdentityEndpoints
{
    public static async Task<Results<Ok, Ok<LoginResponse>, UnauthorizedHttpResult>> Login(
        [FromQuery] bool useCookies,
        [FromBody] LoginRequest request,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ApplicationDbContext dbContext,
        IJwtTokenGenerator tokenGenerator
    )
    {
        // Check uniqueness
        var exists = await dbContext.Users.AnyAsync(u => u.Email == request.Email);
        if(!exists)
            return TypedResults.Unauthorized();

        // Retrieve the user
        var user = await userManager.FindByEmailAsync(request.Email);
        if(user is null)
            return TypedResults.Unauthorized();

        // Check password
        var passwordMatch = await userManager.CheckPasswordAsync(user, request.Password);
        if(!passwordMatch)
            return TypedResults.Unauthorized();

        // Sign him in or generate the token
        if(useCookies)
        {
            await signInManager.SignInAsync(user, true);
            return TypedResults.Ok();
        }

        List<Claim> claims = [
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email!)
        ];

        var roles = (await userManager.GetRolesAsync(user))
            .Select(r => new Claim(ClaimTypes.Role, r));

        claims.AddRange(roles);
        var token = tokenGenerator.GenerateToken(claims);

        // Return
        return TypedResults.Ok(new LoginResponse(token));
    }

    public record struct LoginRequest(
        string Email,
        string Password
    );

    public record struct LoginResponse(
        string Token
    );
}
