using Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Shared.EF.Database;

namespace Identity.API.Data;

internal class ApplicationUsersSeeder(UserManager<ApplicationUser> userManager) : IDbSeeder<ApplicationDbContext>
{
    public async Task SeedAsync(ApplicationDbContext context)
    {
        // Seed users
        var user = new ApplicationUser()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "admin",
            Email = "admin@email.com",
            NormalizedEmail = "ADMIN@EMAIL.COM"
        };

        await userManager.CreateAsync(user, "String 1");
    }
}
