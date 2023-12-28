namespace Identity.API.Options;

public class CookieOptions
{
    public const string SectionName = nameof(CookieOptions);
    public int ExpirationInMinutes { get; set; }
}
