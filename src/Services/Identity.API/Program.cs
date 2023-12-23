using Identity.API.Apis;
using Identity.API.Extensions;
using Identity.API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddIdentityServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.MapDefaultEndpoints();

app.UseAuthentication();

app.UseAuthorization();

app
    .MapGroup("account")
    .WithTags("Identity")
    .MapIdentityApi<ApplicationUser>();

app.MapIdentityEndpoints();

app.Run();
