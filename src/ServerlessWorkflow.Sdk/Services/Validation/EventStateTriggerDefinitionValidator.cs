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
using System.Linq;

namespace ServerlessWorkflow.Sdk.Services.Validation
{

    /// <summary>
    /// Represents a service used to validate <see cref="EventStateTriggerDefinition"/>s
    /// </summary>
    internal class EventStateTriggerDefinitionValidator
        : AbstractValidator<EventStateTriggerDefinition>
    {

        /// <summary>
        /// Initializes a new <see cref="EventStateTriggerDefinitionValidator"/>
        /// </summary>
        /// <param name="workflow">The <see cref="WorkflowDefinition"/> the <see cref="EventStateTriggerDefinition"/> to validate belongs to</param>
        /// <param name="eventState">The <see cref="EventStateDefinition"/> the <see cref="EventStateTriggerDefinition"/> to validate belongs to</param>
        public EventStateTriggerDefinitionValidator(WorkflowDefinition workflow, EventStateDefinition eventState)
        {
            this.Workflow = workflow;
            this.EventState = eventState;
            this.RuleForEach(t => t.Actions)
                .SetValidator(new ActionDefinitionValidator(this.Workflow))
                .When(t => t.Actions != null && t.Actions.Any())
                .WithErrorCode($"{nameof(EventStateTriggerDefinition)}.{nameof(EventStateTriggerDefinition.Actions)}");
            this.RuleFor(t => t.Events)
                .NotEmpty()
                .WithErrorCode($"{nameof(EventStateTriggerDefinition)}.{nameof(EventStateTriggerDefinition.Events)}");
            this.RuleForEach(t => t.Events)
                .Must(ReferenceExistingEvent)
                .When(t => t.Events != null && t.Events.Any())
                .WithErrorCode($"{nameof(EventStateTriggerDefinition)}.{nameof(EventStateTriggerDefinition.Events)}")
                .WithMessage(eventRef => $"Failed to find an event with name '{eventRef}'");
            this.RuleForEach(t => t.Events)
                .Must(BeConsumed)
                .When(t => t.Events != null && t.Events.Any())
                .WithErrorCode($"{nameof(EventStateTriggerDefinition)}.{nameof(EventStateTriggerDefinition.Events)}")
                .WithMessage(eventRef => $"The event with name '{eventRef}' must be of kind '{EnumHelper.Stringify(EventKind.Consumed)}' to be used in an event state trigger");
        }

        /// <summary>
        /// Gets the <see cref="WorkflowDefinition"/> the <see cref="EventStateTriggerDefinition"/> to validate belongs to
        /// </summary>
        protected WorkflowDefinition Workflow { get; }

        /// <summary>
        /// Gets the <see cref="EventStateDefinition"/> the <see cref="EventStateTriggerDefinition"/> to validate belongs to
        /// </summary>
        protected EventStateDefinition EventState { get; }

        /// <summary>
        /// Determines whether or not the specified <see cref="EventDefinition"/> exists
        /// </summary>
        /// <param name="eventName">The name of the <see cref="EventDefinition"/> to check</param>
        /// <returns>A boolean indicating whether or not the specified <see cref="EventDefinition"/> exists</returns>
        protected virtual bool ReferenceExistingEvent(string eventName)
        {
            return this.Workflow.TryGetEvent(eventName, out _);
        }

        /// <summary>
        /// Determines whether or not the specified <see cref="EventDefinition"/> is of kind <see cref="EventKind.Consumed"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="EventDefinition"/> to check</param>
        /// <returns>A boolean indicating whether or not the specified <see cref="EventDefinition"/> of kind <see cref="EventKind.Consumed"/></returns>
        protected virtual bool BeConsumed(string name)
        {
            if (!this.Workflow.TryGetEvent(name, out EventDefinition e))
                return false;
            return e.Kind == EventKind.Consumed;
        }

    }

}
