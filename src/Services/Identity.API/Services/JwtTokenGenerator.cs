using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity.API.Models;
using Identity.API.Options;
using Identity.API.Services.Abstraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Identity.API.Services;

public class JwtTokenGenerator(IOptions<JwtOptions> options, IServiceProvider serviceProvider) : IJwtTokenGenerator
{
    private readonly JwtOptions _jwtOptions = options.Value;
    public async Task<JwtResult> GenerateAsync(ApplicationUser user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(""));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: await GetClaims(user),
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationInMinutes),
            signingCredentials: credentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.WriteToken(token);
        var refreshToken = "";

        return new(jwtToken, refreshToken);
    }

    private async Task<Claim[]> GetClaims(ApplicationUser user)
    {
        var scope = serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var claims = new List<Claim>()
        {
            new (ClaimTypes.NameIdentifier, user.Id),
            new (ClaimTypes.Name, user.UserName!)
        };
        var roles = await userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
        return [.. claims];
    }
}
