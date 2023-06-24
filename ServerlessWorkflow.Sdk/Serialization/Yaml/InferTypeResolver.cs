using YamlDotNet.Core.Events;

namespace ServerlessWorkflow.Sdk.Serialization.Yaml;

/// <summary>
/// Represents an <see cref="INodeTypeResolver"/> used to infer node types from deserialized values
/// </summary>
public class InferTypeResolver
    : INodeTypeResolver
{

    /// <inheritdoc/>
    public bool Resolve(NodeEvent? nodeEvent, ref Type currentType)
    {
        if (nodeEvent is Scalar scalar)
        {
            if (scalar.Style == ScalarStyle.SingleQuoted || scalar.Style == ScalarStyle.DoubleQuoted)
            {
                currentType = typeof(string);
                return true;
            }
            if (bool.TryParse(scalar.Value, out _))
            {
                currentType = typeof(bool);
                return true;
            }
            if (byte.TryParse(scalar.Value, out _))
            {
                currentType = typeof(byte);
                return true;
            }
            if (short.TryParse(scalar.Value, out _))
            {
                currentType = typeof(short);
                return true;
            }
            if (int.TryParse(scalar.Value, out _))
            {
                currentType = typeof(int);
                return true;
            }
            if (long.TryParse(scalar.Value, out _))
            {
                currentType = typeof(long);
                return true;
            }
            if (decimal.TryParse(scalar.Value, out _))
            {
                currentType = typeof(decimal);
                return true;
            }
        }
        return false;
    }

}
