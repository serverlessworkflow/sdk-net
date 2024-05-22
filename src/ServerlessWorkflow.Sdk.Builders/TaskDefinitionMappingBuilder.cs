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


using Neuroglia;

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="ITaskDefinitionMappingBuilder"/> interface
/// </summary>
public class TaskDefinitionMappingBuilder
    : ITaskDefinitionMappingBuilder
{

    /// <summary>
    /// Gets a name/value mapping of the tasks the workflow is made out of
    /// </summary>
    protected EquatableDictionary<string, TaskDefinition>? Tasks { get; set; }

    /// <inheritdoc/>
    public virtual ITaskDefinitionMappingBuilder Do(string name, Action<IGenericTaskDefinitionBuilder> setup)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new GenericTaskDefinitionBuilder();
        setup(builder);
        this.Tasks ??= [];
        this.Tasks[name] = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual EquatableDictionary<string, TaskDefinition> Build()
    {
        if (this.Tasks == null || this.Tasks.Count < 1) throw new NullReferenceException("The task must define at least one subtask");
        return this.Tasks;
    }

}