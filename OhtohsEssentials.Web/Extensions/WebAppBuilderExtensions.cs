namespace OhtohsEssentials.Web.Extensions;

public static class WebAppBuilderExtensions
{
    //example usage: [EnableRateLimiting("FixedWindowPerIp")]

    //builder.Services.AddRateLimiter(options =>
    //{
    //    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    //    options.AddPolicy("FixedWindowPerIp", httpContext =>
    //    {
    //        if (httpContext.Connection.RemoteIpAddress == null)
    //            return RateLimitPartition.GetNoLimiter("noip");

    //        var ip = httpContext.Connection.RemoteIpAddress?.ToString()!;

    //        return RateLimitPartition.GetFixedWindowLimiter(
    //            partitionKey: ip,
    //            factory: _ => new FixedWindowRateLimiterOptions
    //            {
    //                PermitLimit = 5,
    //                Window = TimeSpan.FromMinutes(1),
    //                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
    //                QueueLimit = 0
    //            });
    //    });
    //});
}
