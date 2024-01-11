using Catalog.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddCatalogServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapDefaultEndpoints();
}
else
{
    app.UseHttpsRedirection();
}

app.UseOutputCache();

app.MapGroup("/books")
    .WithTags("Catalog API")
    .MapCatalogEndpoints();

app.MapGroup("/authors")
    .WithTags("Authors")
    .MapAuthorsEndpoints();

app.MapGroup("/tags")
    .WithTags("Tags")
    .MapTagsEndpoints();

app.Run();
