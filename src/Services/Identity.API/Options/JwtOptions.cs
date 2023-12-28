namespace Identity.API.Options;

public class JwtOptions
{
    public const string SectionName = nameof(JwtOptions);
    public required string SecretKey { get; init; }
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public int ExpirationInMinutes { get; init; }
}
