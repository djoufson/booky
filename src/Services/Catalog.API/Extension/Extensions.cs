using Catalog.API.Infra;
using Catalog.API.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Shared.EF.Database;

namespace Catalog.API.Extension;

internal static class Extensions
{
    public static IServiceCollection AddCatalogServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CatalogDbContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("Postgresql"));
        });

        services.AddMigration<CatalogDbContext, BooksCatalogSeeder>();
        return services;
    }
}
