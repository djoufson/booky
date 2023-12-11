using Aspire.ServiceDefaults.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Aspire.ServiceDefaults;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddDefaultAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var identitySection = configuration.GetSection(IdentityOptions.SectionName);

        if (!identitySection.Exists())
        {
            return services;
        }

        // Configure the basic Authentication
        JsonWebTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

        services.AddAuthentication().AddJwtBearer(options =>
        {
            var identityUrl = identitySection.GetRequiredValue(nameof(IdentityOptions.Url));
            var audience = identitySection.GetRequiredValue(nameof(IdentityOptions.Audience));

            options.Authority = identityUrl;
            options.RequireHttpsMetadata = false;
            options.Audience = audience;
            options.TokenValidationParameters.ValidateAudience = false;
        });

        services.AddAuthorization();
        return services;
    }
}
