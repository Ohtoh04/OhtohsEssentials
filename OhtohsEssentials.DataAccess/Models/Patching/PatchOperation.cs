using OhtohsEssentials.DataAccess.Enums;

namespace OhtohsEssentials.DataAccess.Models.Patching;

/// <summary>
/// Represents a single patch operation that defines a change to be applied to a specific property.
/// </summary>
/// <remarks>
/// This class follows the JSON Patch specification pattern where each operation
/// specifies the target path, the operation type, and optionally a value.
/// </remarks>
public class PatchOperation
{
    /// <summary>
    /// Gets or sets the JSON Pointer path that identifies the property to be updated.
    /// </summary>
    /// <value>The path to the property, typically in the format "/propertyName".</value>
    /// <remarks>
    /// Examples:
    /// - "/FirstName" - Updates the FirstName property
    /// - "/Address/City" - Updates a nested property
    /// - "/Tags/0" - Updates an element in an array
    /// </remarks>
    public string Path { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the value to be used for the operation.
    /// </summary>
    /// <value>The value to set for replace/add operations, or null for remove operations.</value>
    /// <remarks>
    /// - For Replace operations: The new value to assign to the property
    /// - For Add operations: The value to add to an array or property
    /// - For Remove operations: This should be null as no value is needed
    /// - Can be null to explicitly set a property to null
    /// </remarks>
    public object? Value { get; set; }

    /// <summary>
    /// Gets or sets the type of operation to perform.
    /// </summary>
    /// <value>The operation type that determines how the patch should be applied.</value>
    /// <remarks>
    /// The operation type defines the action to take:
    /// - Replace: Updates an existing property with a new value
    /// - Remove: Clears or removes a property (sets to null or removes from collection)
    /// - Add: Adds a new value to an array or sets a property
    /// </remarks>
    public OperationType Type { get; set; }
}
