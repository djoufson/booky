namespace Catalog.API.Extensions;

public static class AsyncEnumerableExtensions
{
    public static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> asyncEnumerable)
    {
        var results = new List<T>();
        await foreach (var item in asyncEnumerable)
        {
            results.Add(item);
        }

        return results;
    }
}
