namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Defines the fundamentals of an object that can be extended
/// </summary>
public interface IExtendable
{

    /// <summary>
    /// Gets/sets a name/definition mapping of the component's extensions, if any
    /// </summary>
    JsonObject? Extensions { get; init; }

}