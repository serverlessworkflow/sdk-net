using System.Xml;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace ServerlessWorkflow.Sdk.Serialization.Yaml;

/// <summary>
/// Represents the <see cref="IYamlTypeConverter"/> used to serialize ISO 8601 <see cref="TimeSpan"/>s
/// </summary>
public class Iso8601TimeSpanSerializer
    : IYamlTypeConverter
{

    /// <inheritdoc/>
    public virtual bool Accepts(Type type) => type == typeof(TimeSpan);

    /// <inheritdoc/>
    public virtual object ReadYaml(IParser parser, Type type) => throw new NotImplementedException();

    /// <inheritdoc/>
    public virtual void WriteYaml(IEmitter emitter, object? value, Type type)
    {
        emitter.Emit(new Scalar(XmlConvert.ToString((TimeSpan)value!)));
    }

}