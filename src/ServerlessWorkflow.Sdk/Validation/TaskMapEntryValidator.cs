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

using FluentValidation;
using ServerlessWorkflow.Sdk.Models;

namespace ServerlessWorkflow.Sdk.Validation;

/// <summary>
/// Represents the <see cref="IValidator"/> used to validate <see cref="TaskDefinition"/> map entries
/// </summary>
public class TaskMapEntryValidator
    : AbstractValidator<MapEntry<string, TaskDefinition>>
{

    /// <inheritdoc/>
    public TaskMapEntryValidator(IServiceProvider serviceProvider, ComponentDefinitionCollection? components, IDictionary<string, TaskDefinition>? tasks)
    {
        this.ServiceProvider = serviceProvider;
        this.Components = components;
        this.Tasks = tasks;
        this.RuleFor(t => t.Value)
            .SetValidator(t => new TaskDefinitionValidator(this.ServiceProvider, this.Components, this.Tasks));
    }

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// Gets the configured reusable components
    /// </summary>
    protected ComponentDefinitionCollection? Components { get; }

    /// <summary>
    /// Gets a ke/definition mapping of all tasks in the scope of the task to validate
    /// </summary>
    protected IDictionary<string, TaskDefinition>? Tasks { get; }

}
