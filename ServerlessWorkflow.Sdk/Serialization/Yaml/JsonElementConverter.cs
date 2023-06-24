using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace ServerlessWorkflow.Sdk.Serialization;

/// <summary>
/// Represents the <see cref="IYamlTypeConverter"/> used to serialize <see cref="JsonElement"/>s
/// </summary>
public class JsonElementConverter
    : IYamlTypeConverter
{

    /// <inheritdoc/>
    public virtual bool Accepts(Type type) => typeof(JsonElement).IsAssignableFrom(type);

    /// <inheritdoc/>
    public virtual object ReadYaml(IParser parser, Type type) => throw new NotImplementedException();

    /// <inheritdoc/>
    public virtual void WriteYaml(IEmitter emitter, object? value, Type type)
    {
        var token = (JsonElement?)value;
        if (token == null) return;
        switch (token.Value!.ValueKind)
        {
            case JsonValueKind.True:
                emitter.Emit(new Scalar(AnchorName.Empty, TagName.Empty, "true", ScalarStyle.Plain, true, true));
                break;
            case JsonValueKind.False:
                emitter.Emit(new Scalar(AnchorName.Empty, TagName.Empty, "false", ScalarStyle.Plain, true, true));
                break;
            default:
                var node = Serializer.Json.SerializeToNode(token);
                new JsonNodeConverter().WriteYaml(emitter, node, type);
                break;
        }
    }

}
