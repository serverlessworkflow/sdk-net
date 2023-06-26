namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build metadata containers
/// </summary>
/// <typeparam name="TContainer">The type of the <see cref="IMetadataContainerBuilder{TContainer}"/></typeparam>
public interface IMetadataContainerBuilder<TContainer>
    where TContainer : class, IMetadataContainerBuilder<TContainer>
{

    /// <summary>
    /// Gets the container's metadata
    /// </summary>
    DynamicMapping? Metadata { get; }

    /// <summary>
    /// Adds the specified metadata
    /// </summary>
    /// <param name="key">The metadata key</param>
    /// <param name="value">The metadata value</param>
    /// <returns>The configured container</returns>
    TContainer WithMetadata(string key, object value);

    /// <summary>
    /// Adds the specified metadata
    /// </summary>
    /// <param name="metadata">An <see cref="IDictionary{TKey, TValue}"/> representing the container's metadata</param>
    /// <returns>The configured container</returns>
    TContainer WithMetadata(IDictionary<string, object> metadata);

}
