using Microsoft.AspNetCore.Http;

namespace OhtohsEssentials.Web.Infrastructure.Middleware;

public class DefaultCachingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        context.Response.GetTypedHeaders()
            .CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue
            {
                NoCache = true,
                NoStore = true
            };

        await next(context);
    }
}

/*
 Usage
 
builder.Services.AddTransient<DefaultCachingMiddleware>();
// ...
app.UseMiddleware<DefaultCachingMiddleware>();

*/
