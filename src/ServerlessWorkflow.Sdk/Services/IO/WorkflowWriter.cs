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

using Microsoft.Extensions.DependencyInjection;

namespace ServerlessWorkflow.Sdk.Services.IO;

/// <summary>
/// Represents the default implementation of the <see cref="IWorkflowWriter"/> interface
/// </summary>
public class WorkflowWriter
    : IWorkflowWriter
{

    /// <inheritdoc/>
    public virtual void Write(WorkflowDefinition workflow, Stream stream, string format = WorkflowDefinitionFormat.Yaml)
    {
        if (workflow == null) throw new ArgumentNullException(nameof(workflow));
        if (stream == null)throw new ArgumentNullException(nameof(stream));
        var input = format switch
        {
            WorkflowDefinitionFormat.Json => Serialization.Serializer.Json.Serialize(workflow),
            WorkflowDefinitionFormat.Yaml => Serialization.Serializer.Yaml.Serialize(workflow),
            _ => throw new NotSupportedException($"The specified workflow definition format '{format}' is not supported"),
        };
        using var streamWriter = new StreamWriter(stream, leaveOpen: true);
        streamWriter.Write(input);
        streamWriter.Flush();
    }

    /// <summary>
    /// Creates a new default instance of the <see cref="IWorkflowWriter"/> interface
    /// </summary>
    /// <returns>A new <see cref="IWorkflowWriter"/></returns>
    public static IWorkflowWriter Create()
    {
        var services = new ServiceCollection();
        services.AddServerlessWorkflow();
        return services.BuildServiceProvider().GetRequiredService<IWorkflowWriter>();
    }

}
