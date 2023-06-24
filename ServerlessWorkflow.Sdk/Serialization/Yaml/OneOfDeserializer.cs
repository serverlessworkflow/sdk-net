namespace ServerlessWorkflow.Sdk.Serialization.Yaml;

/// <summary>
/// Represents an <see cref="INodeDeserializer"/> used to deserialize <see cref="OneOf{T1, T2}"/> instances
/// </summary>
public class OneOfDeserializer
    : INodeDeserializer
{

    /// <summary>
    /// Initializes a new <see cref="OneOfDeserializer"/>
    /// </summary>
    /// <param name="inner">The inner <see cref="INodeDeserializer"/></param>
    public OneOfDeserializer(INodeDeserializer inner)
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
        var oneOfType = expectedType.GetGenericType(typeof(OneOf<,>));
        if(oneOfType == null || !oneOfType.IsAssignableFrom(expectedType)) return this.Inner.Deserialize(reader, expectedType, nestedObjectDeserializer, out value);
        var t1 = oneOfType.GetGenericArguments()[0];
        var t2 = oneOfType.GetGenericArguments()[1];
        value = nestedObjectDeserializer(reader, typeof(object));
        var json = Serializer.Json.Serialize(value);
        try
        {
            value = Serializer.Json.Deserialize(json, t1);
        }
        catch
        {
            value = Serializer.Json.Deserialize(json, t2);
        }
        return true;
    }

}
