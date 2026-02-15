using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using static OhtohsEssentials.Web.Infrastructure.Constants.InfrastructureConstants;

namespace OhtohsEssentials.Web.Infrastructure.ActionFilters;

/// <summary>
/// An action filter that computes and adds an ETag header to the HTTP response based on the action result.
/// </summary>
/// <remarks>
/// This filter checks the <see cref="HttpContext.Items"/> for a precomputed ETag or computes a new one
/// based on the serialized response object. If the incoming request contains an "If-None-Match" header
/// matching the computed ETag, the response is short-circuited with a 304 Not Modified status.
/// </remarks>
public class ETagFilterAttribute : ActionFilterAttribute
{
    /// <summary>
    /// Called before the action method is executed.
    /// </summary>
    /// <param name="context">The context for the action executing.</param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
    }

    /// <summary>
    /// Called after the action method has executed.
    /// </summary>
    /// <param name="context">The context for the action executed.</param>
    /// <remarks>
    /// If the action result is an <see cref="ObjectResult"/> and its value is not null,
    /// this method computes or retrieves an ETag for the response. The ETag is added to the response headers.
    /// If the request contains an "If-None-Match" header that matches the ETag, the response status
    /// is set to 304 Not Modified, bypassing the normal response body.
    /// </remarks>
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult objectResult)
        {
            if (objectResult.Value == null) return;

            var httpContext = context.HttpContext;

            string? eTag = null;
            if (httpContext.Items.TryGetValue(ContextItems.ETag, out var value))
            {
                eTag = value?.ToString()
                    ?? ComputeETag(objectResult.Value);
            }
            else
            {
                eTag = ComputeETag(objectResult.Value);
            }
            httpContext.Response.Headers.Append(HttpHeaders.ETag, eTag);

            if (httpContext.Request.Headers.TryGetValue(HttpHeaders.IfNoneMatch, out var headerValue) && headerValue.ToString() == eTag)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status304NotModified);
            }
        }

        base.OnActionExecuted(context);
    }

    /// <summary>
    /// Computes a strong ETag for the given object by serializing it to JSON and hashing the result using SHA-1.
    /// </summary>
    /// <param name="value">The object to compute the ETag for.</param>
    /// <returns>A string representing the ETag, enclosed in double quotes.</returns>
    private string ComputeETag(object value)
    {
        var json = JsonSerializer.Serialize(value);
        using var sha = SHA1.Create();
        var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(json));
        return $"\"{Convert.ToBase64String(hash)}\"";
    }
}
