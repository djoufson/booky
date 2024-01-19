using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity.API.Options;
using Identity.API.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Identity.API.Services;

internal class JwtTokenGenerator(IOptions<JwtOptions> _jwtOptionsGenerator, IDateTimeProvider dateTimeProvider) : IJwtTokenGenerator
{
    private readonly JwtOptions _jwtOptions = _jwtOptionsGenerator.Value;
    public string GenerateToken(IEnumerable<Claim> claims)
    {
        SigningCredentials signingCredentials = new(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtOptions.Secret)),
            SecurityAlgorithms.HmacSha256
        );

        JwtSecurityToken securityToken = new(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            expires: dateTimeProvider.UtcNow.AddDays(_jwtOptions.ExpirationInDays),
            claims: claims,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
