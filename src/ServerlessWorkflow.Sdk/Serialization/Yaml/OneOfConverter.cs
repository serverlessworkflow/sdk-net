// Copyright © 2024-Present The Serverless Workflow Specification Authors
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

using Neuroglia.Serialization.Json;
using Neuroglia.Serialization.Yaml;
using ServerlessWorkflow.Sdk.Models;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace ServerlessWorkflow.Sdk.Serialization.Yaml;

/// <summary>
/// Represents the <see cref="IYamlTypeConverter"/> used to serialize and deserialize <see cref="OneOf{T1, T2}"/> instances
/// </summary>
public class OneOfConverter
    : IYamlTypeConverter
{

    /// <inheritdoc/>
    public virtual bool Accepts(Type type) => type.GetGenericType(typeof(OneOf<,>)) != null;

    /// <inheritdoc/>
    public virtual object? ReadYaml(IParser parser, Type type, ObjectDeserializer rootDeserializer) => throw new NotImplementedException();

    /// <inheritdoc/>
    public virtual void WriteYaml(IEmitter emitter, object? value, Type type, ObjectSerializer rootSerializer)
    {
        if (value == null || value is not IOneOf oneOf)
        {
            emitter.Emit(new Scalar(null, null, string.Empty));
            return;
        }
        var toSerialize = oneOf.GetValue();
        if (toSerialize == null)
        {
            emitter.Emit(new Scalar(null, null, string.Empty));
            return;
        }
        var jsonNode = JsonSerializer.Default.SerializeToNode(toSerialize);
        new JsonNodeTypeConverter().WriteYaml(emitter, jsonNode, toSerialize.GetType(), rootSerializer);
    }

}