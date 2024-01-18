using Basket.API.Data;
using Shared.Extensions;

namespace Basket.API.Extensions;

public static class Extensions
{
    public static IHostApplicationBuilder AddBasketServices(this IHostApplicationBuilder builder)
    {
        builder.AddServiceDefaults();
        builder.Services.AddControllers();
        builder.AddNpgsqlDbContext<BasketDbContext>("BasketDb");
        builder.Services.AddMigration<BasketDbContext>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAuthentication()
            .AddBearerToken()
            .AddCookie();
        builder.Services.AddAuthorization();
        return builder;
    }
}
