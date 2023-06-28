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

using FluentValidation;

namespace ServerlessWorkflow.Sdk.Services.Validation;

/// <summary>
/// Represents the service used to validate <see cref="ErrorHandlerDefinition"/>s
/// </summary>
public class ErrorHandlerDefinitionValidator
    : AbstractValidator<ErrorHandlerDefinition>
{

    /// <summary>
    /// Initializes a new <see cref="ErrorHandlerDefinitionValidator"/>
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> the <see cref="ErrorHandlerDefinition"/>s to validate belong to</param>
    /// <param name="state">The state definition the <see cref="ErrorHandlerDefinition"/>s to validate belong to</param>
    public ErrorHandlerDefinitionValidator(WorkflowDefinition workflow, StateDefinition state)
    {
        this.Workflow = workflow;
        this.State = state;
        this.RuleFor(h => h.Error)
            .NotEmpty();
        this.RuleFor(h => h.Code)
            .Empty()
            .When(h => h.Error == "*")
            .WithMessage("The 'Code' property cannot be set when the 'Error' property has been set to '*'");
        this.RuleFor(h => h.End)
            .NotNull()
            .When(h => h.Transition == null);
        this.RuleFor(h => h.Transition!)
            .NotNull()
            .When(h => h.End == null)
            .SetValidator(new TransitionDefinitionValidator(workflow));
    }

    /// <summary>
    /// Gets the <see cref="WorkflowDefinition"/> the <see cref="ErrorHandlerDefinition"/>s to validate belong to
    /// </summary>
    protected WorkflowDefinition Workflow { get; }

    /// <summary>
    /// Gets the state definition the <see cref="ErrorHandlerDefinition"/>s to validate belong to
    /// </summary>
    protected StateDefinition State { get; }

}
