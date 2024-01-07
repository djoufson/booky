using Basket.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddBasketServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.MapDefaultEndpoints();

app.Run();
