using System.Security.Claims;
using Identity.API.Services.Interfaces;

namespace Identity.API.Services;

internal class JwtTokenGenerator : IJwtTokenGenerator
{
    public string GenerateToken(IEnumerable<Claim> claims)
    {
        return "token";
    }
}
