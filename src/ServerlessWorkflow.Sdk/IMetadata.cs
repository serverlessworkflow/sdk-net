namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Defines the fundamentals of an object that exposes metadata
/// </summary>
public interface IMetadata
{

    /// <summary>
    /// Gets an <see cref="DynamicMapping"/> that contains the object's metadata
    /// </summary>
    DynamicMapping? Metadata { get; }

}
