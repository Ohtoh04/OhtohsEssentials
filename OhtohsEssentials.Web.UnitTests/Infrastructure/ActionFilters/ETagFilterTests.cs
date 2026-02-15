using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NSubstitute;
using OhtohsEssentials.Web.Infrastructure.ActionFilters;
using static OhtohsEssentials.Web.Infrastructure.Constants.InfrastructureConstants;

namespace OhtohsEssentials.Web.UnitTests.Infrastructure.ActionFilters;

public class ETagFilterTests
{
    [Fact]
    public void AddsETagHeader_WhenResultIsObjectResult()
    {
        // Arrange
        var filter = new ETagFilterAttribute();
        var httpContext = new DefaultHttpContext();
        var obj = new { Asd = "Asd" };
        var result = new ObjectResult(obj);

        var controller = Substitute.For<Controller>();

        var context = new ActionExecutedContext(
            new ActionContext
            {
                HttpContext = httpContext,
                RouteData = new(),
                ActionDescriptor = new()
            },
            new List<IFilterMetadata>(),
            controller);

        context.Result = result;

        // Act
        filter.OnActionExecuted(context);

        // Assert
        httpContext.Response.Headers.ContainsKey(HttpHeaders.ETag).Should().BeTrue();

        var expectedETag = ComputeETag(obj);
        httpContext.Response.Headers[HttpHeaders.ETag].ToString().Should().Be(expectedETag);

        context.Result.Should().BeOfType<ObjectResult>();
    }

    [Fact]
    public void SetsStatus304_WhenIfNoneMatchHeaderMatchesETag()
    {
        // Arrange
        var filter = new ETagFilterAttribute();
        var httpContext = new DefaultHttpContext();
        var obj = new { Name = "Test" };
        var result = new ObjectResult(obj);

        var etag = ComputeETag(obj);
        httpContext.Request.Headers[HttpHeaders.IfNoneMatch] = etag;

        var controller = Substitute.For<Controller>();

        var context = new ActionExecutedContext(
            new ActionContext
            {
                HttpContext = httpContext,
                RouteData = new(),
                ActionDescriptor = new()
            },
            new List<IFilterMetadata>(),
            controller)
        {
            Result = result
        };

        // Act
        filter.OnActionExecuted(context);

        // Assert
        context.Result.Should().BeOfType<StatusCodeResult>()
            .Which.StatusCode.Should().Be(StatusCodes.Status304NotModified);

        httpContext.Response.Headers.ContainsKey(HttpHeaders.ETag).Should().BeTrue();
        httpContext.Response.Headers[HttpHeaders.ETag].ToString().Should().Be(etag);
    }

    [Fact]
    public void DoesNotModify_WhenResultIsNotObjectResult()
    {
        // Arrange
        var filter = new ETagFilterAttribute();
        var httpContext = new DefaultHttpContext();
        var result = new StatusCodeResult(StatusCodes.Status200OK);

        var controller = Substitute.For<Controller>();

        var context = new ActionExecutedContext(
            new ActionContext
            {
                HttpContext = httpContext,
                RouteData = new(),
                ActionDescriptor = new()
            },
            new List<IFilterMetadata>(),
            controller)
        {
            Result = result
        };

        // Act
        filter.OnActionExecuted(context);

        // Assert
        context.Result.Should().Be(result);
        httpContext.Response.Headers.ContainsKey(HttpHeaders.ETag).Should().BeFalse();
    }

    private string ComputeETag(object value)
    {
        var json = JsonSerializer.Serialize(value);
        using var sha = SHA1.Create();
        var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(json));
        return $"\"{Convert.ToBase64String(hash)}\"";
    }
}
