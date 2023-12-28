using Identity.API.Models;

namespace Identity.API.Services.Abstraction;

public interface IJwtTokenGenerator
{ 
    Task<JwtResult> GenerateAsync(ApplicationUser user);
}

public record JwtResult(
    string Token,
    string RefreshToken);
