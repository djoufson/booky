var builder = DistributedApplication.CreateBuilder(args);
var postgres = builder.AddPostgresContainer("postgres");
var redisCache = builder.AddRedisContainer("redis");

var catalogDb = postgres.AddDatabase("CatalogDb");
var identityDb = postgres.AddDatabase("IdentityDb");
var basketDb = postgres.AddDatabase("BasketDb");

var catalogApi = builder
    .AddProject<Projects.Catalog_API>("catalog-api")
    .WithReference(redisCache)
    .WithReference(catalogDb);

var identityApi = builder
    .AddProject<Projects.Identity_API>("identity-api")
    .WithReference(identityDb);

var basketApi = builder
    .AddProject<Projects.Basket_API>("basket-api")
    .WithReference(basketDb);

var api = builder
    .AddProject<Projects.LoadBalancer>("api")
    .WithReference(identityApi)
    .WithReference(catalogApi)
    .WithReference(basketApi);

builder
    .AddProject<Projects.Web>("web")
    .WithReference(api);

builder.Build().Run();
