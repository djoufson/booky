using System.Security.Claims;

namespace Identity.API.Services.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(IEnumerable<Claim> claims);
}
