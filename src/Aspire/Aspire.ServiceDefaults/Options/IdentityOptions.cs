namespace Aspire.ServiceDefaults.Options;

public class IdentityOptions
{
    public static string SectionName => "Identity";
    public required string Url { get; set; }
    public required string Audience { get; set; }
}
