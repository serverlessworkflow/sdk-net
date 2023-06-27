namespace ServerlessWorkflow.Sdk.Serialization;

/// <summary>
/// Represents the <see cref="IYamlTypeConverter"/> used to serialize and deserialize <see cref="OneOf{T1, T2}"/> instances
/// </summary>
public class OneOfConverter
    : IYamlTypeConverter
{

    /// <inheritdoc/>
    public virtual bool Accepts(Type type) => typeof(IOneOf).IsAssignableFrom(type);

    /// <inheritdoc/>
    public virtual object ReadYaml(IParser parser, Type type) => throw new NotImplementedException();

    /// <inheritdoc/>
    public virtual void WriteYaml(IEmitter emitter, object? value, Type type)
    {
        if (value == null) return;
        var toSerialize = value;
        if (value is IOneOf oneOf) toSerialize = oneOf.GetValue();
        var node = Serializer.Json.SerializeToNode(toSerialize);
        new JsonNodeConverter().WriteYaml(emitter, node, typeof(JsonNode));
    }

}
