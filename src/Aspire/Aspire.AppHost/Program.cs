var builder = DistributedApplication.CreateBuilder(args);
var postgres = builder.AddPostgresContainer("postgres");

var catalogDb = postgres.AddDatabase("CatalogDb");
var identityDb = postgres.AddDatabase("IdentityDb");

builder
    .AddProject<Projects.Catalog_API>("catalog-api")
    .WithReference(catalogDb);

builder
    .AddProject<Projects.Identity_API>("identity-api")
    .WithReference(identityDb);

builder.Build().Run();
