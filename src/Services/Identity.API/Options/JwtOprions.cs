namespace Identity.API.Options;

public sealed class JwtOptions
{
    public const string SectionName = nameof(JwtOptions);
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string Secret { get; init; }
    public int ExpirationInDays { get; init; }
}
