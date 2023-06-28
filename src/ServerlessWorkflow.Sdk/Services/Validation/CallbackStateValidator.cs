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
/// Represents a service used to validate <see cref="CallbackStateDefinition"/>s
/// </summary>
public class CallbackStateValidator
    : StateDefinitionValidator<CallbackStateDefinition>
{

    /// <summary>
    /// Initializes a new <see cref="CallbackStateValidator"/>
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> to validate</param>
    public CallbackStateValidator(WorkflowDefinition workflow) 
        : base(workflow)
    {
        this.RuleFor(s => s.Action)
            .NotNull()
            .WithErrorCode($"{nameof(CallbackStateDefinition)}.{nameof(CallbackStateDefinition.Action)}");
        this.RuleFor(s => s.Action!)
            .SetValidator(new ActionDefinitionValidator(workflow))
            .When(s => s.Action != null)
            .WithErrorCode($"{nameof(CallbackStateDefinition)}.{nameof(CallbackStateDefinition.Action)}");
        this.RuleFor(s => s.EventRef)
            .NotEmpty()
            .WithErrorCode($"{nameof(CallbackStateDefinition)}.{nameof(CallbackStateDefinition.EventRef)}");
        this.RuleFor(s => s.EventRef!)
            .Must(ReferenceExistingEvent)
            .When(s => !string.IsNullOrWhiteSpace(s.EventRef))
            .WithErrorCode($"{nameof(CallbackStateDefinition)}.{nameof(CallbackStateDefinition.EventRef)}")
            .WithMessage((state, eventRef) => $"Failed to find the event with name '{eventRef}' defined by the callback state with name '{state.Name}'");
    }

}
