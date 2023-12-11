var builder = DistributedApplication.CreateBuilder(args);
var postgres = builder.AddPostgresContainer("postgres");

var catalogDb = postgres.AddDatabase("CatalogDb");
builder
    .AddProject<Projects.Catalog_API>("catalog-api")
    .WithReference(catalogDb);

builder.Build().Run();
