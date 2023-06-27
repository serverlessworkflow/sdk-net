namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Defines the fundamentals of an extensible object
/// </summary>
internal interface IExtensible
{

    /// <summary>
    /// Gets an <see cref="IDictionary{TKey, TValue}"/> containing the object's extension data, if any
    /// </summary>
    IDictionary<string, object>? ExtensionData { get; }

}
