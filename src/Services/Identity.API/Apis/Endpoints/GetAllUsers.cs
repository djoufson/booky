using Identity.API.Data;
using Identity.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Apis.Endpoints;

public partial class IdentityEndpoints
{
    public static async Task<Results<Ok<ApplicationUser[]>,BadRequest>> GetAllUsers(ApplicationDbContext context)
    {
        var users = await context.Users.ToArrayAsync();
        return TypedResults.Ok(users);
    }
}
