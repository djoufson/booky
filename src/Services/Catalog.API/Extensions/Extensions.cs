using Catalog.API.Infra.Data;
using Catalog.API.Infrastructure;
using Catalog.API.Options;
using Catalog.API.Services;
using Shared.Extensions;

namespace Catalog.API.Extensions;

internal static class Extensions
{
    public static IHostApplicationBuilder AddCatalogServices(this IHostApplicationBuilder builder, IConfiguration configuration)
    {
        // The commented code was used without Aspire
        // services.AddDbContext<CatalogDbContext>(opt =>
        // {
        //     opt.UseNpgsql(configuration.GetConnectionString("Postgresql"));
        // });

        builder.Services.AddScoped<ImageService>();
        builder.AddNpgsqlDbContext<CatalogDbContext>("CatalogDb");
        builder.Services.AddMigration<CatalogDbContext, BooksCatalogSeeder>();

        builder.Services.AddSwaggerGen();
        builder.Services.AddProblemDetails();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOptions<CatalogOptions>()
            .BindConfiguration(nameof(CatalogOptions));
        return builder;
    }
}
