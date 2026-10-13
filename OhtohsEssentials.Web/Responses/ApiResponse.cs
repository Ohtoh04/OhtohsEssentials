namespace OhtohsEssentials.Web.Responses;

/// <summary>
/// Generic API response envelope containing data, links, and metadata.
/// </summary>
/// <typeparam name="T">Type of the result payload.</typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// The actual response result (nullable for error scenarios).
    /// </summary>
    public T? Result { get; set; }

    /// <summary>
    /// HATEOAS links related to the current resource.
    /// </summary>
    public List<Link> Links { get; set; } = new();

    /// <summary>
    /// Arbitrary response details.
    /// </summary>
    public Dictionary<string, object> Details { get; set; } = new();

    /// <summary>
    /// Request profiling data.
    /// </summary>
    public ProfilingData? Profiling { get; set; }
}
