/*
 * Copyright 2021-Present The Serverless Workflow Specification Authors
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using FluentValidation;
using ServerlessWorkflow.Sdk.Models;

namespace ServerlessWorkflow.Sdk.Services.Validation
{
    /// <summary>
    /// Represents a service used to validate <see cref="EventCaseDefinition"/>s
    /// </summary>
    internal class EventCaseDefinitionValidator
        : SwitchCaseDefinitionValidator<EventCaseDefinition>
    {

        /// <summary>
        /// Initializes a new <see cref="EventCaseDefinitionValidator"/>
        /// </summary>
        /// <param name="workflow">The <see cref="WorkflowDefinition"/> the <see cref="EventCaseDefinition"/> to validate belongs to</param>
        /// <param name="state">The <see cref="SwitchStateDefinition"/> the <see cref="EventCaseDefinition"/> to validate belongs to</param>
        public EventCaseDefinitionValidator(WorkflowDefinition workflow, SwitchStateDefinition state)
            : base(workflow, state)
        {
            this.RuleFor(c => c.Event)
                .NotEmpty()
                .WithErrorCode($"{nameof(EventCaseDefinition)}.{nameof(EventCaseDefinition.Event)}");
            this.RuleFor(c => c.Event)
                .Must(ReferenceExistingEvent)
                .When(c => !string.IsNullOrWhiteSpace(c.Event))
                .WithErrorCode($"{nameof(EventCaseDefinition)}.{nameof(EventCaseDefinition.Event)}");
        }

        /// <summary>
        /// Determines whether or not the specified <see cref="EventDefinition"/> exists
        /// </summary>
        /// <param name="eventName">The name of the <see cref="EventDefinition"/> to check</param>
        /// <returns>A boolean indicating whether or not the specified <see cref="EventDefinition"/> exists</returns>
        protected virtual bool ReferenceExistingEvent(string eventName)
        {
            return this.Workflow.TryGetEvent(eventName, out _);
        }

    }

}
