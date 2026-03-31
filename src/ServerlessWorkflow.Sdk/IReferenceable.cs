namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Defines the fundamentals of a referenceable object
/// </summary>
public interface IReferenceable
{

    /// <summary>
    /// Gets an URI, if any, that references the object's definition
    /// </summary>
    Uri? Ref { get; }

}
