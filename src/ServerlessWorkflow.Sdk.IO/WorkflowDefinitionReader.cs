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

using Microsoft.Extensions.DependencyInjection;
using Neuroglia.Serialization;

namespace ServerlessWorkflow.Sdk.IO;

/// <summary>
/// Represents the default implementation of the <see cref="IWorkflowDefinitionReader"/> interface
/// </summary>
/// <param name="jsonSerializer">The service used to serialize/deserialize objects to/from JSON</param>
/// <param name="yamlSerializer">The service used to serialize/deserialize objects to/from JSON</param>
public class WorkflowDefinitionReader(IJsonSerializer jsonSerializer, IYamlSerializer yamlSerializer)
    : IWorkflowDefinitionReader
{

    /// <summary>
    /// Gets the service used to serialize/deserialize objects to/from JSON
    /// </summary>
    protected IJsonSerializer JsonSerializer { get; } = jsonSerializer;

    /// <summary>
    /// Gets the service used to serialize/deserialize objects to/from YAML
    /// </summary>
    protected IYamlSerializer YamlSerializer { get; } = yamlSerializer;

    /// <inheritdoc/>
    public virtual Task<WorkflowDefinition> ReadAsync(Stream stream, WorkflowDefinitionReaderOptions? options = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(stream);
        using var reader = new StreamReader(stream);
        var input = reader.ReadToEnd();
        var workflow = (input.TrimStart().StartsWith('{') && input.TrimEnd().EndsWith('}')
            ? this.JsonSerializer.Deserialize<WorkflowDefinition>(input)
            : this.YamlSerializer.Deserialize<WorkflowDefinition>(input))
            ?? throw new NullReferenceException();
        return Task.FromResult(workflow);
    }

    /// <summary>
    /// Creates a new <see cref="IWorkflowDefinitionReader"/>
    /// </summary>
    /// <returns>A new <see cref="IWorkflowDefinitionReader"/></returns>
    public static IWorkflowDefinitionReader Create()
    {
        var services = new ServiceCollection();
        services.AddServerlessWorkflowIO();
        return services.BuildServiceProvider().GetRequiredService<IWorkflowDefinitionReader>();
    }

}