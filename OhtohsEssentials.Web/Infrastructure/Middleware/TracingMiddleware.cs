using Microsoft.AspNetCore.Http;

namespace OhtohsEssentials.Web.Infrastructure.Middleware;

public class TracingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        context.Response.Headers["TraceId"] = context.TraceIdentifier;
        await next(context);
    }
}
