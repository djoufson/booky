using Catalog.API.Infra.Data;
using Catalog.API.Infrastructure;
using Catalog.API.Services;
using Shared.Extensions;

namespace Catalog.API.Extension;

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

        builder.Services.AddProblemDetails();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        return builder;
    }
}
