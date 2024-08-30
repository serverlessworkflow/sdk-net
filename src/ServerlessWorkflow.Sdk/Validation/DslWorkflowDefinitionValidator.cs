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
/// Represents the <see cref="IValidator"/> used to validate <see cref="WorkflowDefinition"/>s
/// </summary>
public class DslWorkflowDefinitionValidator
    : AbstractValidator<WorkflowDefinition>
{

    /// <inheritdoc/>
    public DslWorkflowDefinitionValidator(IServiceProvider serviceProvider)
    {
        this.ServiceProvider = serviceProvider;
        this.RuleFor(w => w.Use)
            .SetValidator(new ComponentDefinitionCollectionValidator(this.ServiceProvider)!);
        this.RuleForEach(w => w.Do)
            .SetValidator(w => new TaskMapEntryValidator(this.ServiceProvider, w.Use, w.Do.ToDictionary(kvp => kvp.Key, kvp => kvp.Value)));
        this.RuleFor(w => w.TimeoutReference!)
            .Must(ReferenceAnExistingTimeout)
            .When(w => !string.IsNullOrWhiteSpace(w.TimeoutReference))
            .WithMessage(ValidationErrors.UndefinedTimeout);
    }

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// Determines whether or not the specified retry policy is defined
    /// </summary>
    /// <param name="workflow">The workflow to check</param>
    /// <param name="name">The name of the timeout to check</param>
    /// <returns>A boolean indicating whether or not the specified retry policy is defined</returns>
    protected virtual bool ReferenceAnExistingTimeout(WorkflowDefinition workflow, string name) => workflow.Use?.Timeouts?.ContainsKey(name) == true;

}
