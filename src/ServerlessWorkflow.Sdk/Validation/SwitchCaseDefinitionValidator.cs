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
using ServerlessWorkflow.Sdk.Properties;

namespace ServerlessWorkflow.Sdk.Validation;

/// <summary>
/// Represents the <see cref="IValidator"/> used to validate <see cref="SwitchCaseDefinition"/>s
/// </summary>
public class SwitchCaseDefinitionValidator
    : AbstractValidator<MapEntry<string, SwitchCaseDefinition>>
{

    /// <inheritdoc/>
    public SwitchCaseDefinitionValidator(IServiceProvider serviceProvider, ComponentDefinitionCollection? components, IDictionary<string, TaskDefinition>? tasks)
    {
        this.ServiceProvider = serviceProvider;
        this.Components = components;
        this.Tasks = tasks;
        this.RuleFor(c => c.Value.Then)
            .Must(ReferenceAnExistingTask)
            .When(NotAFlowDirective)
            .WithName(c => c.Key)
            .WithMessage(ValidationErrors.UndefinedTask);
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

    /// <summary>
    /// Determines whether or not the specified task is defined in the actual scope
    /// </summary>
    /// <param name="name">The name of the task to check</param>
    /// <returns>A boolean indicating whether or not the specified task is defined in the actual scope</returns>
    protected virtual bool ReferenceAnExistingTask(string? name) => !string.IsNullOrWhiteSpace(name) && this.Tasks != null && this.Tasks.ContainsKey(name);

    /// <summary>
    /// Determines whether or not the case's then is a flow directive
    /// </summary>
    /// <param name="case">The case to check</param>
    /// <returns>A boolean indicating whether or not the case's then is a flow directive</returns>
    protected virtual bool NotAFlowDirective(MapEntry<string, SwitchCaseDefinition> @case)
    {
        return @case.Value.Then switch
        {
            null or "" or FlowDirective.Continue or FlowDirective.End or FlowDirective.End => false,
            _ => true
        };
    }

}