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
using ServerlessWorkflow.Sdk.Models.Tasks;
using ServerlessWorkflow.Sdk.Properties;

namespace ServerlessWorkflow.Sdk.Validation;

/// <summary>
/// Represents the <see cref="IValidator"/> used to validate <see cref="TaskDefinition"/>s
/// </summary>
public class TaskDefinitionValidator
    : AbstractValidator<TaskDefinition>
{

    /// <inheritdoc/>
    public TaskDefinitionValidator(IServiceProvider serviceProvider, ComponentDefinitionCollection? components, IDictionary<string, TaskDefinition>? tasks)
    {
        this.ServiceProvider = serviceProvider;
        this.Components = components;
        this.Tasks = tasks;
        this.RuleFor(t => t.Then)
            .Must(ReferenceAnExistingTask)
            .When(NotAFlowDirective)
            .WithMessage(ValidationErrors.UndefinedTask);
        this.RuleFor(t => t.TimeoutReference!)
            .Must(ReferenceAnExistingTimeout)
            .When(t => !string.IsNullOrWhiteSpace(t.TimeoutReference))
            .WithMessage(ValidationErrors.UndefinedTimeout);
        this.When(t => t is CallTaskDefinition, () =>
        {
            this.RuleFor(t => (CallTaskDefinition)t)
                .SetValidator(t => new CallTaskDefinitionValidator(this.ServiceProvider, this.Components));
        });
        this.When(t => t is DoTaskDefinition, () =>
        {
            this.RuleFor(t => (DoTaskDefinition)t)
                .SetValidator(t => new DoTaskDefinitionValidator(this.ServiceProvider, this.Components));
        });
        this.When(t => t is ForkTaskDefinition, () =>
        {
            this.RuleFor(t => (ForkTaskDefinition)t)
                .SetValidator(t => new ForkTaskDefinitionValidator(this.ServiceProvider, this.Components));
        });
        this.When(t => t is RaiseTaskDefinition, () =>
        {
            this.RuleFor(t => (RaiseTaskDefinition)t)
                .SetValidator(t => new RaiseTaskDefinitionValidator(this.ServiceProvider, this.Components));
        });
        this.When(t => t is SwitchTaskDefinition, () =>
        {
            this.RuleFor(t => (SwitchTaskDefinition)t)
                .SetValidator(t => new SwitchTaskDefinitionValidator(this.ServiceProvider, this.Components, this.Tasks));
        });
        this.When(t => t is TryTaskDefinition, () =>
        {
            this.RuleFor(t => (TryTaskDefinition)t)
               .SetValidator(t => new TryTaskDefinitionValidator(this.ServiceProvider, this.Components));
        });
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
    /// Determines whether or not the task's then is a flow directive
    /// </summary>
    /// <param name="task">The task to check</param>
    /// <returns>A boolean indicating whether or not the task's then is a flow directive</returns>
    protected virtual bool NotAFlowDirective(TaskDefinition task)
    {
        return task.Then switch
        {
            null or "" or FlowDirective.Continue or FlowDirective.End or FlowDirective.End => false,
            _ => true
        };
    }

    /// <summary>
    /// Determines whether or not the specified retry policy is defined
    /// </summary>
    /// <param name="name">The name of the timeout to check</param>
    /// <returns>A boolean indicating whether or not the specified retry policy is defined</returns>
    protected virtual bool ReferenceAnExistingTimeout(string name) => this.Components?.Timeouts?.ContainsKey(name) == true;

}
