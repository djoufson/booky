using Identity.API.Data;
using Identity.API.Middlewares;
using Identity.API.Models;
using Identity.API.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.EF.Database;
using Shared.Extensions;

namespace Identity.API.Extensions;

public static class Extensions
{
    public static IHostApplicationBuilder AddIdentityServices(this IHostApplicationBuilder builder, IConfiguration configuration)
    {
        if(builder.Environment.IsProduction())
        {
            var npgslConn = configuration.GetConnectionString("Postgresql")!;
            var options = configuration.GetRequiredSection(PostgresqlOptions.SectionName).Get<PostgresqlOptions>();
            npgslConn = npgslConn
                .Replace("[0]", options.UserId)
                .Replace("[1]", options.UserPassword)
                .Replace("[2]", options.Server)
                .Replace("[3]", options.Port.ToString());


            Console.WriteLine($"--> User ID: {options.UserId}");
            Console.WriteLine($"--> Password: {options.UserPassword}");
            Console.WriteLine($"--> Server: {options.Server}");
            Console.WriteLine($"--> Port: {options.Port}");
            Console.WriteLine($"--> Conn string: {npgslConn}");


            builder.Services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseNpgsql(npgslConn);
            });
            builder.Services.AddMigration<ApplicationDbContext>();
        }
        else
        {
            builder.AddNpgsqlDbContext<ApplicationDbContext>("IdentityDb");
            builder.Services.AddMigration<ApplicationDbContext, ApplicationUsersSeeder>();
        }

        builder.AddServiceDefaults();
        builder.Services.AddAuthorizationBuilder();
        builder.Services.AddScoped<UserIdMiddleware>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services
            .AddIdentity<ApplicationUser, IdentityRole>()
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
