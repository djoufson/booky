using Basket.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddBasketServices();

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
