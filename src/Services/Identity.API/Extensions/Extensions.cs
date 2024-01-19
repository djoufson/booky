using System.Text;
using Identity.API.Data;
using Identity.API.Middlewares;
using Identity.API.Models;
using Identity.API.Options;
using Identity.API.Services;
using Identity.API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.Extensions;

namespace Identity.API.Extensions;

public static class Extensions
{
    public static IHostApplicationBuilder AddIdentityServices(this IHostApplicationBuilder builder, IConfiguration configuration)
    {
        if (builder.Environment.IsProduction())
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
        builder.Services
            .AddAuthentication()
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, (options) =>
            {
                var jwtSettings = configuration.GetRequiredSection(JwtOptions.SectionName).Get<JwtOptions>()!;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                };
                options.IncludeErrorDetails = true;
            });

        builder.Services.AddAuthorization();
        builder.Services.AddScoped<UserIdMiddleware>();
        builder.Services.Configure<JwtOptions>(configuration.GetRequiredSection(JwtOptions.SectionName));
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
