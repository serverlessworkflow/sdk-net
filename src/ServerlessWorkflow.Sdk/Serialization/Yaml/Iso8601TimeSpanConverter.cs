namespace ServerlessWorkflow.Sdk.Serialization.Yaml;

/// <summary>
/// Represents an <see cref="INodeDeserializer"/> used to deserialize ISO8601 <see cref="TimeSpan"/>s
/// </summary>
public class Iso8601TimeSpanConverter
    : INodeDeserializer
{

    /// <summary>
    /// Initializes a new <see cref="Iso8601TimeSpanConverter"/>
    /// </summary>
    /// <param name="inner">The inner <see cref="INodeDeserializer"/></param>
    public Iso8601TimeSpanConverter(INodeDeserializer inner)
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
        if (expectedType != typeof(TimeSpan)&& expectedType != typeof(TimeSpan?)) return this.Inner.Deserialize(reader, expectedType, nestedObjectDeserializer, out value);
        if (!this.Inner.Deserialize(reader, typeof(string), nestedObjectDeserializer, out value))return false;
        value = Iso8601TimeSpan.Parse((string)value!);
        return true;
    }

}
