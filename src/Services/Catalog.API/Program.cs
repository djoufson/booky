using Catalog.API.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCatalogServices();
builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("/api/catalog")
    .WithTags("Catalog API")
    .MapCatalogEndpoints();

app.Run();
