namespace ServerlessWorkflow.Sdk.Serialization.Yaml;

/// <summary>
/// Represents an <see cref="INodeDeserializer"/> used to deserialize <see cref="IExtensible"/> instances
/// </summary>
public class IExtensibleDeserializer
    : INodeDeserializer
{

    /// <summary>
    /// Initializes a new <see cref="IExtensibleDeserializer"/>
    /// </summary>
    /// <param name="inner">The inner <see cref="INodeDeserializer"/></param>
    public IExtensibleDeserializer(INodeDeserializer inner)
    {
        this.Inner = inner;
    }

    /// <summary>
    /// Gets the inner <see cref="INodeDeserializer"/>
    /// </summary>
    protected INodeDeserializer Inner { get; }

    /// <inheritdoc/>
    public virtual bool Deserialize(IParser reader, Type expectedType, Func<IParser, Type, object?> nestedObjectDeserializer, out object? value)
    {
        if(!typeof(IExtensible).IsAssignableFrom(expectedType)) return this.Inner.Deserialize(reader, expectedType, nestedObjectDeserializer, out value);
        var succeeded = this.Inner.Deserialize(reader, typeof(IDictionary<string, object>), nestedObjectDeserializer, out value);
        var json = Serializer.Json.Serialize(value);
        if (succeeded) value = Serializer.Json.Deserialize(json, expectedType);
        return succeeded;
    }

}
