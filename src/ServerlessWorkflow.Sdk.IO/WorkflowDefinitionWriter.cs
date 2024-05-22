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
/// Represents the default implementation of the <see cref="IWorkflowDefinitionWriter"/> interface
/// </summary>
/// /// <param name="jsonSerializer">The service used to serialize/deserialize objects to/from JSON</param>
/// <param name="yamlSerializer">The service used to serialize/deserialize objects to/from JSON</param>
public class WorkflowDefinitionWriter(IJsonSerializer jsonSerializer, IYamlSerializer yamlSerializer)
    : IWorkflowDefinitionWriter
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
    public virtual async Task WriteAsync(WorkflowDefinition workflow, Stream stream, string format = WorkflowDefinitionFormat.Yaml, CancellationToken cancellationToken = default)
    {
        if (workflow == null) throw new ArgumentNullException(nameof(workflow));
        if (stream == null) throw new ArgumentNullException(nameof(stream));
        var input = format switch
        {
            WorkflowDefinitionFormat.Json => this.JsonSerializer.SerializeToText(workflow),
            WorkflowDefinitionFormat.Yaml => this.YamlSerializer.SerializeToText(workflow),
            _ => throw new NotSupportedException($"The specified workflow definition format '{format}' is not supported"),
        };
        using var streamWriter = new StreamWriter(stream, leaveOpen: true);
        await streamWriter.WriteAsync(input).ConfigureAwait(false);
        await streamWriter.FlushAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a new default instance of the <see cref="IWorkflowDefinitionWriter"/> interface
    /// </summary>
    /// <returns>A new <see cref="IWorkflowDefinitionWriter"/></returns>
    public static IWorkflowDefinitionWriter Create()
    {
        var services = new ServiceCollection();
        services.AddServerlessWorkflowIO();
        return services.BuildServiceProvider().GetRequiredService<IWorkflowDefinitionWriter>();
    }

}