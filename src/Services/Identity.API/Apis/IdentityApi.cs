using Identity.API.Apis.Endpoints;

namespace Identity.API.Apis;

public static class IdentityApi
{
    public static IEndpointRouteBuilder MapIdentityEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("state", IdentityEndpoints.GetState)
            .RequireAuthorization();
        app.MapGet("users", IdentityEndpoints.GetAllUsers);
        app.MapPut("users", IdentityEndpoints.UpdateProfile)
            .RequireAuthorization();
        app.MapPatch("users", IdentityEndpoints.UpdateProfile)
            .RequireAuthorization(); // Doing the same as the PUT version
        app.MapPut("upgrade", IdentityEndpoints.UpgradeToAuthor)
            .RequireAuthorization();

        app.MapPut("upgrade/pro", () => "");
        return app;
    }
}
