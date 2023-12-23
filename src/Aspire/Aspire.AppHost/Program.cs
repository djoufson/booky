var builder = DistributedApplication.CreateBuilder(args);
var postgres = builder.AddPostgresContainer("postgres");
var rabbitMq = builder.AddRabbitMQContainer("rabbitmq");

var catalogDb = postgres.AddDatabase("CatalogDb");
var identityDb = postgres.AddDatabase("IdentityDb");

var catalogApi = builder
    .AddProject<Projects.Catalog_API>("catalog-api")
    .WithReference(catalogDb)
    .WithReference(rabbitMq);

var identityApi = builder
    .AddProject<Projects.Identity_API>("identity-api")
    .WithReference(identityDb)
    .WithReference(rabbitMq);

var api = builder
    .AddProject<Projects.LoadBalancer>("api")
    .WithReference(identityApi)
    .WithReference(catalogApi);

builder
    .AddProject<Projects.Web>("web")
    .WithReference(api);

builder.Build().Run();
