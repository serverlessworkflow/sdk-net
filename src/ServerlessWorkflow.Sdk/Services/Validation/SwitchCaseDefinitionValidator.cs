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
/// Represents the base class of all services used to validate <see cref="SwitchCaseDefinition"/>s
/// </summary>
/// <typeparam name="TCondition">The type of <see cref="SwitchCaseDefinition"/> to validate</typeparam>
public abstract class SwitchCaseDefinitionValidator<TCondition>
    : AbstractValidator<TCondition>
    where TCondition : SwitchCaseDefinition
{

    /// <summary>
    /// Initializes a new <see cref="SwitchCaseDefinitionValidator{TCondition}"/>
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> the <see cref="SwitchCaseDefinition"/> to validate belongs to</param>
    /// <param name="state">The <see cref="SwitchStateDefinition"/> the <see cref="SwitchCaseDefinition"/> to validate belongs to</param>
    protected SwitchCaseDefinitionValidator(WorkflowDefinition workflow, SwitchStateDefinition state)
    {
        this.Workflow = workflow;
        this.State = state;
        this.RuleFor(s => s.Transition!)
            .Must(ReferenceExistingState)
            .When(s => s.Transition != null)
            .WithMessage((state, transition) => $"Failed to find the state '{transition.NextState}' to transition to");
        this.RuleFor(c => c.Transition)
            .NotNull()
            .When(c => c.End == null)
            .WithErrorCode($"{nameof(DataCaseDefinition)}.{nameof(DataCaseDefinition.Transition)}")
            .WithMessage($"One of either '{nameof(DataCaseDefinition.Transition)}' or '{nameof(DataCaseDefinition.End)}' properties must be set");
        this.RuleFor(c => c.End)
            .NotNull()
            .When(c => c.Transition == null)
            .WithErrorCode($"{nameof(DataCaseDefinition)}.{nameof(DataCaseDefinition.End)}")
            .WithMessage($"One of either '{nameof(DataCaseDefinition.Transition)}' or '{nameof(DataCaseDefinition.End)}' properties must be set");

    }

    /// <summary>
    /// Gets the <see cref="WorkflowDefinition"/> the <see cref="SwitchCaseDefinition"/> to validate belongs to
    /// </summary>
    protected WorkflowDefinition Workflow { get; }

    /// <summary>
    /// Gets the <see cref="SwitchStateDefinition"/> the <see cref="SwitchCaseDefinition"/> to validate belongs to
    /// </summary>
    protected SwitchStateDefinition State { get; }

    /// <summary>
    /// Determines whether or not the specified state definition exists
    /// </summary>
    /// <param name="transition">The name of the state definition to check</param>
    /// <returns>A boolean indicating whether or not the specified state definition exists</returns>
    protected virtual bool ReferenceExistingState(TransitionDefinition transition) => this.Workflow.TryGetState(transition.NextState, out _);

    /// <summary>
    /// Determines whether or not the specified state definition exists
    /// </summary>
    /// <param name="stateName">The name of the state definition to check</param>
    /// <returns>A boolean indicating whether or not the specified state definition exists</returns>
    protected virtual bool ReferenceExistingState(string stateName) => this.Workflow.TryGetState(stateName, out _);
}
