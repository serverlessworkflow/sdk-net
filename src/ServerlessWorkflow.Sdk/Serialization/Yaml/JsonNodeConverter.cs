// Copyright © 2023-Present The Serverless Workflow Specification Authors
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using YamlDotNet.Core.Events;

namespace ServerlessWorkflow.Sdk.Serialization;

/// <summary>
/// Represents the <see cref="IYamlTypeConverter"/> used to serialize <see cref="JsonNode"/>s
/// </summary>
public class JsonNodeConverter
    : IYamlTypeConverter
{

    /// <inheritdoc/>
    public virtual bool Accepts(Type type) => typeof(JsonNode).IsAssignableFrom(type);

    /// <inheritdoc/>
    public virtual object ReadYaml(IParser parser, Type type) => throw new NotImplementedException();

    /// <inheritdoc/>
    public virtual void WriteYaml(IEmitter emitter, object? value, Type type) => this.WriteJsonNode(emitter, (JsonNode?)value);

    void WriteJsonNode(IEmitter emitter, JsonNode? node)
    {
        if (node == null) return;
        switch (node)
        {
            case JsonArray jsonArray:
                this.WriteJsonArray(emitter, jsonArray);
                break;
            case JsonObject jsonObject:
                this.WriteJsonObject(emitter, jsonObject);
                break;
            case JsonValue jsonValue:
                this.WriteJsonValue(emitter, jsonValue);
                break;
            default:
                throw new NotSupportedException($"The specified JSON node type '{node.GetType().FullName}' is not supported");
        }
    }

    void WriteJsonArray(IEmitter emitter, JsonArray jsonArray)
    {
        emitter.Emit(new SequenceStart(null, null, false, SequenceStyle.Block));
        foreach (var node in jsonArray)
        {
            this.WriteJsonNode(emitter, node);
        }
        emitter.Emit(new SequenceEnd());
    }

    void WriteJsonObject(IEmitter emitter, JsonObject jsonObject)
    {
        emitter.Emit(new MappingStart(null, null, false, MappingStyle.Block));
        foreach (var property in jsonObject)
        {
            this.WriteJsonProperty(emitter, property);
        }
        emitter.Emit(new MappingEnd());
    }

    void WriteJsonProperty(IEmitter emitter, KeyValuePair<string, JsonNode?> jsonProperty)
    {
        if (jsonProperty.Value == null) return;
        emitter.Emit(new Scalar(jsonProperty.Key));
        this.WriteJsonNode(emitter, jsonProperty.Value);
    }

    void WriteJsonValue(IEmitter emitter, JsonValue jsonValue)
    {
        var value = Serializer.Json.Deserialize<object>(jsonValue);
        if (value == null) return;
        Scalar scalar = value switch
        {
            bool boolValue => new(boolValue.ToString().ToLowerInvariant()),
            DateTime dateTime => new(dateTime.ToString()),
            DateTimeOffset dateTimeOffset => new(dateTimeOffset.ToString()),
            float floatValue => new(AnchorName.Empty, TagName.Empty, floatValue.ToString(), ScalarStyle.Plain, true, false),
            Guid guid => new(guid.ToString()),
            TimeSpan timeSpan => new(Iso8601TimeSpan.Format(timeSpan)),
            JsonElement jsonElement => ConvertToScalar(jsonElement),
            _ => new(AnchorName.Empty, TagName.Empty, value.ToString()!, ScalarStyle.SingleQuoted, true, true)
        };
        emitter.Emit(scalar);
    }

    Scalar ConvertToScalar(JsonElement jsonElement)
    {
        return jsonElement.ValueKind switch
        {
            JsonValueKind.True => new(AnchorName.Empty, TagName.Empty, "true", ScalarStyle.Plain, true, true),
            JsonValueKind.False => new(AnchorName.Empty, TagName.Empty, "false", ScalarStyle.Plain, true, true),
            JsonValueKind.String => new(AnchorName.Empty, TagName.Empty, jsonElement.ToString(), jsonElement.ToString().All(c => char.IsDigit(c) || c == '.' || c == ',') ? ScalarStyle.SingleQuoted : ScalarStyle.Any, true, true),
            _ => new(AnchorName.Empty, TagName.Empty, jsonElement.ToString()!)
        };
    }

}
