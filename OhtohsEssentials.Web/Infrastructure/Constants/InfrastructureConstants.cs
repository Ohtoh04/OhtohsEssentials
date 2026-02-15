namespace OhtohsEssentials.Web.Infrastructure.Constants;

/// <summary>
/// Provides common infrastructure constants used throughout the application, such as HTTP header names and context item keys.
/// </summary>
public static class InfrastructureConstants
{
    /// <summary>
    /// Defines keys used for storing and retrieving values in <see cref="HttpContext.Items"/>.
    /// </summary>
    public static class ContextItems
    {
        /// <summary>
        /// The key used to store or retrieve the ETag value in <see cref="HttpContext.Items"/>.
        /// </summary>
        public const string ETag = "ETag";

        /// <summary>
        /// The key used to store or retrieve the Queue of domain events in <see cref="HttpContext.Items"/>.
        /// </summary>
        /// <remarks>
        /// Context item returns an object of type <see cref="Queue<IDomainEvent>"/>
        /// </remarks>
        public const string DomainEventsQueue = "DomainEventsQueue";
    }

    /// <summary>
    /// Defines HTTP header names commonly used in the application.
    /// </summary>
    public static class HttpHeaders
    {
        /// <summary>
        /// The HTTP header name for ETag.
        /// </summary>
        public const string ETag = "ETag";

        /// <summary>
        /// The HTTP header name for "If-None-Match", which is used for conditional requests with ETags.
        /// </summary>
        public const string IfNoneMatch = "If-None-Match";
    }
}
