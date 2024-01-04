namespace Catalog.API.Extensions;

public static class AsyncEnumerableExtensions
{
    public static async Task<T[]> ToArrayAsync<T>(this IAsyncEnumerable<T> asyncEnumerable)
    {
        var results = new List<T>();
        await foreach (var item in asyncEnumerable)
        {
            results.Add(item);
        }

        return [..results];
    }
}
