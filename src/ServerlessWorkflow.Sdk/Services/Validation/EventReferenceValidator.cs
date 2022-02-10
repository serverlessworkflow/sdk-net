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
    /// Represents the service used to validate <see cref="EventReference"/>s
    /// </summary>
    public class EventReferenceValidator
        : AbstractValidator<EventReference>
    {

        /// <summary>
        /// Initializes a new <see cref="EventReferenceValidator"/>
        /// </summary>
        /// <param name="workflow">The <see cref="WorkflowDefinition"/> the <see cref="EventReference"/>s to validate belong to</param>
        public EventReferenceValidator(WorkflowDefinition workflow)
        {
            this.Workflow = workflow;
            this.RuleFor(e => e.ProduceEvent)
                .NotEmpty()
                .WithErrorCode($"{nameof(EventReference)}.{nameof(EventReference.ProduceEvent)}");
            this.RuleFor(e => e.ProduceEvent)
                .Must(ReferenceExistingEvent)
                .When(e => !string.IsNullOrWhiteSpace(e.ProduceEvent))
                .WithErrorCode($"{nameof(EventReference)}.{nameof(EventReference.ProduceEvent)}")
                .WithMessage(eventRef => $"Failed to find the event with name '{eventRef.ProduceEvent}'");
            this.RuleFor(e => e.ProduceEvent)
                .Must(BeProduced)
                .When(e => !string.IsNullOrWhiteSpace(e.ProduceEvent))
                .WithErrorCode($"{nameof(EventReference)}.{nameof(EventReference.ProduceEvent)}")
                .WithMessage(eventRef => $"The event with name '{eventRef.ProduceEvent}' must be of kind '{EnumHelper.Stringify(EventKind.Produced)}'");
            this.RuleFor(e => e.ConsumeEvent)
                .NotEmpty()
                .WithErrorCode($"{nameof(EventReference)}.{nameof(EventReference.ConsumeEvent)}");
            this.RuleFor(e => e.ConsumeEvent)
                .Must(ReferenceExistingEvent)
                .When(e => !string.IsNullOrWhiteSpace(e.ConsumeEvent))
                .WithErrorCode($"{nameof(EventReference)}.{nameof(EventReference.ConsumeEvent)}")
                .WithMessage(eventRef => $"Failed to find the event with name '{eventRef.ConsumeEvent}'");
            this.RuleFor(e => e.ConsumeEvent)
                .Must(BeConsumed)
                .When(e => !string.IsNullOrWhiteSpace(e.ConsumeEvent))
                .WithErrorCode($"{nameof(EventReference)}.{nameof(EventReference.ConsumeEvent)}")
                .WithMessage(eventRef => $"The event with name '{eventRef.ConsumeEvent}' must be of kind '{EnumHelper.Stringify(EventKind.Consumed)}'");
        }

        /// <summary>
        /// Gets the <see cref="WorkflowDefinition"/> the <see cref="FunctionReference"/>s to validate belong to
        /// </summary>
        protected WorkflowDefinition Workflow { get; }

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
        /// Determines whether or not the specified <see cref="EventDefinition"/> is of kind <see cref="EventKind.Produced"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="EventDefinition"/> to check</param>
        /// <returns>A boolean indicating whether or not the specified <see cref="EventDefinition"/> of kind <see cref="EventKind.Produced"/></returns>
        protected virtual bool BeProduced(string name)
        {
            if (!this.Workflow.TryGetEvent(name, out EventDefinition e))
                return false;
            return e.Kind == EventKind.Produced;
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
