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
