using Identity.API.Data;
using Identity.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Apis.Endpoints;

public partial class IdentityEndpoints
{
    public static async Task<Results<Ok<RegisterResponse>,BadRequest<string>>> Register(
        [FromBody] RegisterRequest request,
        [FromServices] UserManager<ApplicationUser> userManager,
        [FromServices] ApplicationDbContext dbContext
    )
    {
        var exists = await dbContext.Users.AnyAsync(u => u.UserName == request.UserName);
        if(exists)
            return TypedResults.BadRequest("This username is already taken");

        var user = new ApplicationUser()
        {
            UserName = request.UserName,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };

        await userManager.CreateAsync(user, request.Password);
        await dbContext.AddAsync(user);
        await dbContext.SaveChangesAsync();
        var response = new RegisterResponse(
            user.Id,
            user.UserName!,
            user.FirstName,
            user.LastName,
            user.Email!
        );

        return TypedResults.Ok(response);
    }

    public record RegisterRequest(
        string UserName,
        string FirstName,
        string? LastName,
        string Email,
        string Password
    );

    public record RegisterResponse(
        string Id,
        string UserName,
        string FirstName,
        string? LastName,
        string Email
    );
}
