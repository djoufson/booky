using Catalog.API.Infra.Data;
using Catalog.API.Infrastructure;
using Catalog.API.Options;
using Catalog.API.Services;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;

namespace Catalog.API.Extensions;

internal static class Extensions
{
    public static IHostApplicationBuilder AddCatalogServices(this IHostApplicationBuilder builder, IConfiguration configuration)
    {
        if(builder.Environment.IsProduction())
        {
            builder.Services.AddDbContext<CatalogDbContext>(opt =>
            {
                opt.UseNpgsql(configuration.GetConnectionString("Postgresql"));
            });
        }
        else
        {
            builder.AddNpgsqlDbContext<CatalogDbContext>("CatalogDb");
            builder.AddServiceDefaults();
        }

        builder.Services.AddScoped<ImageService>();
        builder.Services.AddMigration<CatalogDbContext, BooksCatalogSeeder>();
        builder.Services.AddSwaggerGen();
        builder.Services.AddProblemDetails();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOptions<CatalogOptions>()
            .BindConfiguration(nameof(CatalogOptions));
        return builder;
    }
}
