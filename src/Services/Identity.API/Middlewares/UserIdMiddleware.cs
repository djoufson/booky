using System.Security.Claims;
using Identity.API.Utilities;

namespace Identity.API.Middlewares;

public class UserIdMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if(userIdClaim is not null)
        {
            context.Request.Headers.TryAdd(Constants.HeadersKeys.USER_ID, userIdClaim.Value);
        }

        await next(context);
    }
}
