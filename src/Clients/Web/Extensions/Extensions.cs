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

        builder.Services.AddScoped<CatalogService>();
        builder.Services.AddHttpClient<CatalogService>(cfg =>
        {
            cfg.BaseAddress = new ("http://api/c/");
        });

        return builder;
    }
}
