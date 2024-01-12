namespace Identity.API.Options;

public record struct PostgresqlOptions(string UserId, string UserPassword, string Server, int Port)
{
    public const string SectionName = "Postgresql";
}
