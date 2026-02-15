namespace OhtohsEssentials.DataAccess.Models.Patching;

/// <summary>
/// Represents a custom patch document that contains a collection of patch operations.
/// This document follows a structure similar to JSON Patch (RFC 6902) for applying partial updates.
/// </summary>
/// <remarks>
/// The PatchDocument provides a structured way to specify multiple update operations
/// on different properties, each with its own operation type and value.
/// </remarks>
public class PatchDocument
{
    /// <summary>
    /// Gets or sets the list of patch operations to be applied to the target entity.
    /// </summary>
    /// <remarks>
    /// Each operation in the list represents a single change to be made.
    /// Operations are applied in the order they appear in the list.
    /// </remarks>
    public List<PatchOperation> Operations { get; set; } = new();
}
