using Catalog.API.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddCatalogServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.MapDefaultEndpoints();

app.MapGroup("/api/catalog")
    .WithTags("Catalog API")
    .MapCatalogEndpoints();

app.MapGroup("/api/authors")
    .WithTags("Authors")
    .MapAuthorsEndpoints();

app.Run();
