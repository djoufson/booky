using Identity.API.Data;
using Identity.API.Middlewares;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Shared.EF.Database;
using Shared.Extensions;

namespace Identity.API.Extensions;

public static class Extensions
{
    public static IHostApplicationBuilder AddIdentityServices(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<ApplicationDbContext>("IdentityDb");
        builder.Services.AddAuthorizationBuilder();
        builder.Services.AddScoped<UserIdMiddleware>();
        builder.Services.AddMigration<ApplicationDbContext, ApplicationUsersSeeder>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services
            .AddIdentityApiEndpoints<ApplicationUser>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        return builder;
    }
}

internal class ApplicationUsersSeeder() : IDbSeeder<ApplicationDbContext>
{
    public Task SeedAsync(ApplicationDbContext context)
    {
        // Seed users
        return Task.CompletedTask;
    }
}
