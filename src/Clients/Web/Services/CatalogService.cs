using System.Text;
using Shared.Contracts.Catalog.Dtos;

namespace Web.Services;

public class CatalogService(HttpClient client)
{
    private readonly StringBuilder _sb = new();
    public async Task<BookDto[]> GetBooksAsync(string? search, string[] tags)
    {
        try
        {
            _sb.Clear();
            _sb.Append("books?");
            bool added = false;
            if(tags.Length > 0)
            {
                _sb
                    .Append("tags=")
                    .AppendJoin("&tags=", tags);
                added = true;
            }
            if(!string.IsNullOrWhiteSpace(search))
            {
                if(added)
                    _sb.Append('&');

                _sb
                    .Append("search=")
                    .Append(search);
            }

            return await client.GetFromJsonAsync<BookDto[]>(_sb.ToString()) ?? [];
        }
        catch (Exception)
        {
            return [];
        }
    }

    public async Task<TagDto[]> GetAllTagsAsync()
    {
        try
        {
            return await client.GetFromJsonAsync<TagDto[]>("tags") ?? [];
        }
        catch (Exception)
        {
            return [];
        }
    }

    public async Task<BookDto?> GetBookAsync(string slug)
    {
        try
        {
            return await client.GetFromJsonAsync<BookDto>($"books/{slug}");
        }
        catch (Exception)
        {
            return null;
        }
    }
}
