var builder = DistributedApplication.CreateBuilder(args);
var postgres = builder.AddPostgresContainer("postgres");
var rabbitMq = builder.AddRabbitMQContainer("rabbitmq");

var catalogDb = postgres.AddDatabase("CatalogDb");
var identityDb = postgres.AddDatabase("IdentityDb");

builder
    .AddProject<Projects.Catalog_API>("catalog-api")
    .WithReference(catalogDb)
    .WithReference(rabbitMq);

builder
    .AddProject<Projects.Identity_API>("identity-api")
    .WithReference(identityDb)
    .WithReference(rabbitMq);

builder.Build().Run();
