using Identity.API.Data;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.EF.Database;
using Shared.Extensions;

namespace Identity.API.Extension;

public static class Extensions
{
    public static IHostApplicationBuilder AddIdentityServices(this IHostApplicationBuilder builder)
    {
        // Used without Aspire
        // builder.Services.AddDbContext<ApplicationDbContext>(opt =>
        // {
        //     opt.UseNpgsql("IdentityDb");
        // });
        // builder.Services.AddIdentity<ApplicationUser, IdentityRole>();
        builder.AddNpgsqlDbContext<ApplicationDbContext>("IdentityDb");
        builder.Services.AddAuthorizationBuilder();
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

internal class ApplicationUsersSeeder : IDbSeeder<ApplicationDbContext>
{
    public Task SeedAsync(ApplicationDbContext context)
    {
        return Task.CompletedTask;
    }
}
