using Microsoft.AspNetCore.OutputCaching;
using StackExchange.Redis;

namespace Catalog.API.Caching;

public sealed class RedisOutputCacheStore(IConnectionMultiplexer redis) : IOutputCacheStore
{
    private readonly IDatabase _database = redis.GetDatabase();

    public async ValueTask EvictByTagAsync(string tag, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(tag);
        var cachedKeys = await _database.SetMembersAsync(tag);
        var keys = cachedKeys
            .Select(x => (RedisKey)x.ToString())
            .Append((RedisKey) tag)
            .ToArray();

        await _database.KeyDeleteAsync(keys);
    }

    public async ValueTask<byte[]?> GetAsync(string key, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        return await _database.StringGetAsync(key);
    }

    public async ValueTask SetAsync(string key, byte[] value, string[]? tags, TimeSpan validFor, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        ArgumentNullException.ThrowIfNull(value);

        foreach (var tag in tags ?? [])
        {
            await _database.SetAddAsync(tag, key);
        }

        await _database.StringSetAsync(key, value, validFor);
    }
}
