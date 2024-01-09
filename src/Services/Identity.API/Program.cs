using Identity.API.Apis;
using Identity.API.Extensions;
using Identity.API.Middlewares;
using Identity.API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.MapDefaultEndpoints();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<UserIdMiddleware>();

app
    .MapGroup("account")
    .WithTags("Identity")
    .MapIdentityApi<ApplicationUser>();

app
    .MapGroup("account")
    .WithTags("Identity")
    .MapIdentityEndpoints();

app.Run();
