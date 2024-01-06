using Shared.Contracts.Catalog.Dtos;

namespace Web.Services;

public class CatalogService(HttpClient client)
{
    public async Task<BookDto[]> GetAllBooksAsync()
    {
        try
        {
            return await client.GetFromJsonAsync<BookDto[]>("books") ?? [];
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
