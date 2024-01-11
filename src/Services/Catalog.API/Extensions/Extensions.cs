using Catalog.API.Caching;
using Catalog.API.Infra.Data;
using Catalog.API.Infrastructure;
using Catalog.API.Options;
using Catalog.API.Services;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;
using StackExchange.Redis;

namespace Catalog.API.Extensions;

internal static class Extensions
{
    public static IHostApplicationBuilder AddCatalogServices(
        this IHostApplicationBuilder builder,
        IConfiguration configuration)
    {
        if(builder.Environment.IsProduction())
        {
            builder.Services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));
            builder.Services.AddDbContext<CatalogDbContext>(opt =>
            {
                opt.UseNpgsql(configuration.GetConnectionString("Postgresql"));
            });
        }
        else
        {
            builder.AddServiceDefaults();
            builder.AddRedis("redis");
            builder.AddNpgsqlDbContext<CatalogDbContext>("CatalogDb");
        }

        builder.Services.AddOutputCacheRedis(options =>
        {
            options.AddPolicy(Cache.Policies.Search, x => x.Tag(Cache.Tags.Search).Cache().SetVaryByQuery("search"));
            options.AddPolicy(Cache.Policies.GetAllBooks, x => x.Tag(Cache.Tags.GetAll).Cache());
            options.AddPolicy(Cache.Policies.GetAllTags, x => x.Tag(Cache.Tags.BookGenres).Cache());
            options.AddPolicy(Cache.Policies.Authors, x => x.Tag(Cache.Tags.Authors).Cache());
        });
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
