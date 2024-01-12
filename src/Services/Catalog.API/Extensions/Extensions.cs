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
            var npgslConn = configuration.GetConnectionString("Postgresql")!;
            var options = configuration.GetRequiredSection(PostgresqlOptions.SectionName).Get<PostgresqlOptions>();
            npgslConn = npgslConn
                .Replace("[0]", options.UserId)
                .Replace("[1]", options.UserPassword)
                .Replace("[2]", options.Server)
                .Replace("[3]", options.Port.ToString());

            builder.Services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));
            builder.Services.AddDbContext<CatalogDbContext>(opt =>
            {
                opt.UseNpgsql(npgslConn);
            });
            builder.Services.AddMigration<CatalogDbContext>();
        }

        else
        {
            builder.AddRedis("redis");
            builder.AddNpgsqlDbContext<CatalogDbContext>("CatalogDb");
            builder.Services.AddMigration<CatalogDbContext, BooksCatalogSeeder>();
        }

        builder.AddServiceDefaults();
        builder.Services.AddOutputCacheRedis(options =>
        {
            options.AddPolicy(Cache.Policies.GetBooks, x => x.Tag(Cache.Tags.GetAll).Cache().SetVaryByQuery("search", "tags", "pageNumber", "pageSize"));
            options.AddPolicy(Cache.Policies.GetAllTags, x => x.Tag(Cache.Tags.BookGenres).Cache());
            options.AddPolicy(Cache.Policies.Authors, x => x.Tag(Cache.Tags.Authors).Cache());
        });
        builder.Services.AddScoped<ImageService>();
        builder.Services.AddSwaggerGen();
        builder.Services.AddProblemDetails();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOptions<CatalogOptions>()
            .BindConfiguration(nameof(CatalogOptions));
        return builder;
    }
}
