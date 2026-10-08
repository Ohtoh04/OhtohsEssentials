using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OhtohsEssentials.Core.Exceptions;

namespace OhtohsEssentials.Web.Infrastructure.Middleware;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        ProblemDetails problem;

        if (exception is ApiException apiException)
        {
            problem = new ProblemDetails
            {
                Status = (int)apiException.StatusCode,
                Title = GetTitleForStatusCode(apiException.StatusCode),
                Detail = apiException.Message,
                Instance = context.Request.Path
            };

            problem.Extensions["code"] = apiException.ErrorCode;
        }
        else
        {
            problem = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "Internal Server Error",
                Detail = "An unexpected error occurred.",
                Instance = context.Request.Path
            };
            problem.Extensions["code"] = "INTERNAL_ERROR";
        }

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = problem.Status ?? 500;

        var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var jsonResponse = JsonSerializer.Serialize(problem, jsonOptions);
        await context.Response.WriteAsync(jsonResponse);
    }

    private static string GetTitleForStatusCode(HttpStatusCode statusCode) => statusCode switch
    {
        HttpStatusCode.BadRequest => "Bad Request",
        HttpStatusCode.Unauthorized => "Unauthorized",
        HttpStatusCode.Forbidden => "Forbidden",
        HttpStatusCode.NotFound => "Not Found",
        HttpStatusCode.Conflict => "Conflict",
        HttpStatusCode.PaymentRequired => "Payment Required",
        (HttpStatusCode)418 => "I'm a Teapot",
        _ => "Error"
    };
}
