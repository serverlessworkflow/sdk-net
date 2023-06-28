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
/// Represents a service used to validate <see cref="EventStateDefinition"/>s
/// </summary>
public class EventStateValidator
    : StateDefinitionValidator<EventStateDefinition>
{

    /// <summary>
    /// Initializes a new <see cref="EventStateValidator"/>
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> to validate</param>
    public EventStateValidator(WorkflowDefinition workflow) 
        : base(workflow)
    {
        this.RuleFor(s => s.OnEvents)
            .NotEmpty()
            .WithErrorCode($"{nameof(EventStateDefinition)}.{nameof(EventStateDefinition.OnEvents)}");
        this.RuleForEach(s => s.OnEvents)
            .SetValidator(state => new EventStateTriggerDefinitionValidator(this.Workflow, state))
            .When(s => s.OnEvents != null && s.OnEvents.Any())
            .WithErrorCode($"{nameof(EventStateDefinition)}.{nameof(EventStateDefinition.OnEvents)}");
    }

}
