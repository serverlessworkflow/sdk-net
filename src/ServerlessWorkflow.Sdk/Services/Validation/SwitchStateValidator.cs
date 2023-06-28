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
/// Represents a service used to validate <see cref="SwitchStateDefinition"/>s
/// </summary>
public class SwitchStateValidator
    : StateDefinitionValidator<SwitchStateDefinition>
{

    /// <summary>
    /// Initializes a new <see cref="SwitchStateValidator"/>
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> to validate</param>
    public SwitchStateValidator(WorkflowDefinition workflow)
        : base(workflow)
    {
        this.RuleFor(s => s.DataConditions)
            .NotEmpty()
            .When(s => s.EventConditions == null || !s.EventConditions.Any())
            .WithErrorCode($"{nameof(SwitchStateDefinition)}.{nameof(SwitchStateDefinition.DataConditions)}")
            .WithMessage($"One of either '{nameof(SwitchStateDefinition.DataConditions)}' or '{nameof(SwitchStateDefinition.EventConditions)}' properties must be set");
        this.RuleForEach(s => s.DataConditions)
            .SetValidator(state => new DataCaseDefinitionValidator(this.Workflow, state))
            .When(s => s.DataConditions != null && s.DataConditions.Any())
            .WithErrorCode($"{nameof(SwitchStateDefinition)}.{nameof(SwitchStateDefinition.DataConditions)}");
        this.RuleFor(s => s.EventConditions)
            .NotEmpty()
            .When(s => s.DataConditions == null || !s.DataConditions.Any())
            .WithErrorCode($"{nameof(SwitchStateDefinition)}.{nameof(SwitchStateDefinition.EventConditions)}")
            .WithMessage($"One of either '{nameof(SwitchStateDefinition.DataConditions)}' or '{nameof(SwitchStateDefinition.EventConditions)}' properties must be set");
        this.RuleForEach(s => s.EventConditions)
            .SetValidator(state => new EventCaseDefinitionValidator(this.Workflow, state))
            .When(s => s.EventConditions != null && s.EventConditions.Any())
            .WithErrorCode($"{nameof(SwitchStateDefinition)}.{nameof(SwitchStateDefinition.EventConditions)}");
        this.RuleFor(s => s.DefaultCondition)
            .NotNull()
            .WithErrorCode($"{nameof(SwitchStateDefinition)}.{nameof(SwitchStateDefinition.DefaultCondition)}");
        this.RuleFor(s => s.DefaultCondition)
            .SetValidator(c => new DefaultCaseDefinitionValidator(this.Workflow, c))
            .WithErrorCode($"{nameof(SwitchStateDefinition)}.{nameof(SwitchStateDefinition.DefaultCondition)}");

    }

}
