namespace OhtohsEssentials.DataAccess.Enums;

/// <summary>
/// Defines the types of operations that can be performed in a patch document.
/// </summary>
/// <remarks>
/// These operation types correspond to common CRUD operations and follow
/// the JSON Patch specification conventions.
/// </remarks>
public enum OperationType
{
    /// <summary>
    /// Replaces an existing property with a new value.
    /// </summary>
    /// <remarks>
    /// Use this when you want to update an existing property with a new value.
    /// The target property must exist.
    /// </remarks>
    Replace,

    /// <summary>
    /// Removes a property or clears its value.
    /// </summary>
    /// <remarks>
    /// - For nullable properties: Sets the property to null
    /// - For collections: Removes the specified item
    /// - For required properties: May need special handling
    /// </remarks>
    Remove,

    /// <summary>
    /// Adds a new value to a property or collection.
    /// </summary>
    /// <remarks>
    /// - For arrays/collections: Adds a new item
    /// - For objects: Sets a property value (creates if doesn't exist)
    /// </remarks>
    Add
}
