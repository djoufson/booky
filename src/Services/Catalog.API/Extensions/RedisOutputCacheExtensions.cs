using Catalog.API.Caching;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Catalog.API.Extensions;

public static class RedisOutputCacheExtensions
{
    public static IServiceCollection AddOutputCacheRedis(this IServiceCollection services)
    {
        services.AddOutputCacheRedis(x => {});
        return services;
    }

    public static IServiceCollection AddOutputCacheRedis(this IServiceCollection services, Action<OutputCacheOptions> configureOptions)
    {
        services.AddOutputCache(configureOptions);
        services.RemoveAll<IOutputCacheStore>();
        services.AddSingleton<IOutputCacheStore, RedisOutputCacheStore>();
        return services;
    }
}
