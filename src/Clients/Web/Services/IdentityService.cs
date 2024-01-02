using System.Text.Json;

namespace Web.Services;

public class IdentityService(HttpClient client)
{
    public async Task<AuthResult?> LoginAsync(string email, string password)
    {
        try
        {
            var response = await client.PostAsJsonAsync("login?useCookies=true", new { email, password });
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<AuthResponse>(content);
            }
            else
            {
                // Login failed
            }
        }
        catch (Exception)
        {
        }
        return new AuthResult();
    }
}

record AuthResponse();
public record AuthResult();
