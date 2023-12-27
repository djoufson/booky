using Web.Services;

namespace Web.Extensions;

public static class Extensions
{
    public static IHostApplicationBuilder AddServices(this IHostApplicationBuilder builder)
    {
        builder.AddServiceDefaults();
        builder.Services
            .AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddOptions();
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<CatalogService>();
        builder.Services.AddScoped<IdentityService>();
        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddHttpClient<CatalogService>(cfg =>
        {
            cfg.BaseAddress = new ("http://api/c/");
        });
        builder.Services.AddHttpClient<IdentityService>(cfg =>
        {
            cfg.BaseAddress = new ("http://api/i/account/");
        });

        return builder;
    }
}
