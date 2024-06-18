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

using ServerlessWorkflow.Sdk.Models;
using Neuroglia.Serialization.Json;
using YamlDotNet.Core;

namespace ServerlessWorkflow.Sdk.Serialization.Yaml;

/// <summary>
/// Represents the service used to deserialize <see cref="TaskDefinition"/>s from YAML
/// </summary>
/// <remarks>
/// Initializes a new <see cref="TaskDefinitionYamlDeserializer"/>
/// </remarks>
/// <param name="inner">The inner <see cref="INodeDeserializer"/></param>
public class TaskDefinitionYamlDeserializer(INodeDeserializer inner)
    : INodeDeserializer
{

    /// <summary>
    /// Gets the inner <see cref="INodeDeserializer"/>
    /// </summary>
    protected INodeDeserializer Inner { get; } = inner;
    
    /// <inheritdoc/>
    public virtual bool Deserialize(IParser reader, Type expectedType, Func<IParser, Type, object?> nestedObjectDeserializer, out object? value)
    {
        if (!typeof(TaskDefinition).IsAssignableFrom(expectedType)) return this.Inner.Deserialize(reader, expectedType, nestedObjectDeserializer, out value);
        if (!this.Inner.Deserialize(reader, typeof(Dictionary<object, object>), nestedObjectDeserializer, out value)) return false;
        value = JsonSerializer.Default.Deserialize<TaskDefinition>(JsonSerializer.Default.SerializeToText(value!));
        return true;
    }

}
