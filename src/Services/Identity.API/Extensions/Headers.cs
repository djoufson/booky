namespace Identity.API.Extensions;

public static class HeadersExtensions
{
    public static string GetValue(this IHeaderDictionary headers, string key)
    {
        return headers.TryGetValue(key, out var userId) ? userId! : string.Empty;
    }
}
