using Identity.API.Data;
using Identity.API.Middlewares;
using Identity.API.Models;
using Identity.API.Options;
using Identity.API.Services;
using Identity.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();
        builder.Services.AddScoped<UserIdMiddleware>();
        builder.Services.AddOptions<JwtOptions>(JwtOptions.SectionName);
        builder.Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services
            .AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        return builder;
    }
}
