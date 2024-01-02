using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Contracts.Identity.Dtos;

namespace Web.Services;

public class IdentityService(HttpClient client) : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var response = await client.GetAsync("state");
        if (!response.IsSuccessStatusCode)
            return new AuthenticationState(new ClaimsPrincipal());

        var user = await response.Content.ReadFromJsonAsync<UserDto>();

        if (user is null)
            return new AuthenticationState(new ClaimsPrincipal());

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };
        var identity = new ClaimsIdentity(claims, "Cookies");
        var principal = new ClaimsPrincipal(identity);

        return new AuthenticationState(principal);
    }

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
