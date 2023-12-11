namespace Identity.API.Extension;

public static class Extensions
{
    public static IHostApplicationBuilder AddIdentityServices(this IHostApplicationBuilder builder)
    {        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        return builder;
    }
}
