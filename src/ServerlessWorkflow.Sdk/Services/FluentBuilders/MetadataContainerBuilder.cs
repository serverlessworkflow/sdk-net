namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Represents the base class for all <see cref="IMetadataContainerBuilder{TContainer}"/>
/// </summary>
/// <typeparam name="TContainer">The type of the <see cref="IMetadataContainerBuilder{TContainer}"/></typeparam>
public abstract class MetadataContainerBuilder<TContainer>
    : IMetadataContainerBuilder<TContainer>
    where TContainer : class, IMetadataContainerBuilder<TContainer>
{

    /// <inheritdoc/>
    public virtual DynamicMapping? Metadata { get; protected set; }

    /// <inheritdoc/>
    public virtual TContainer WithMetadata(string key, object value)
    {
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
        this.Metadata ??= new();
        this.Metadata[key] = value;
        return (TContainer)(object)this;
    }

    /// <inheritdoc/>
    public virtual TContainer WithMetadata(IDictionary<string, object> metadata)
    {
        if(metadata == null) throw new ArgumentNullException(nameof(metadata));
        this.Metadata = new(metadata);
        return (TContainer)(object)this;
    }

}
