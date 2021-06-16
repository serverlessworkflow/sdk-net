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
    /// Represents the base class for all <see cref="IValidator"/>s used to validate <see cref="StateDefinition"/>s
    /// </summary>
    /// <typeparam name="TState">The type of <see cref="StateDefinition"/> to validate</typeparam>
    public abstract class StateDefinitionValidator<TState>
        : AbstractValidator<TState>
        where TState : StateDefinition
    {

        /// <summary>
        /// Initializes a new <see cref="StateDefinitionValidator{TState}"/>
        /// </summary>
        /// <param name="workflow">The <see cref="WorkflowDefinition"/> to validate</param>
        protected StateDefinitionValidator(WorkflowDefinition workflow)
        {
            this.Workflow = workflow;
            this.RuleFor(s => s.Name)
                .NotNull();
            this.RuleFor(s => s.CompensatedBy)
                .Must(ReferenceExistingState)
                .When(s => !string.IsNullOrWhiteSpace(s.CompensatedBy))
                .WithMessage((state, stateName) => $"Failed to find the state '{stateName}' to use for compensation");
            this.RuleFor(s => s.Transition)
                .Must(ReferenceExistingState)
                .When(s => s.Transition != null)
                .WithMessage((state, stateName) => $"Failed to find the state '{stateName}' to transition to")
                .Must(DefineCompensationState)
                .When(s => s.Transition != null && s.Transition.Compensate)
                .WithMessage(state => $"The '{nameof(StateDefinition.CompensatedBy)}' property of the state '{state.Name}' must be set when enabling its compensation (in both Transition and End definitions)");
            this.RuleFor(s => s.End)
                .Must(DefineCompensationState)
                .When(s => s.End != null && s.End.Compensate)
                .WithMessage(state => $"The '{nameof(StateDefinition.CompensatedBy)}' property of the state '{state.Name}' must be set when enabling its compensation (in both Transition and End definitions)");
            this.RuleForEach(s => s.Errors)
                .SetValidator((s, e) => new ErrorHandlerDefinitionValidator(this.Workflow, s));
            this.RuleFor(s => s.UseForCompensation)
                .Must(BeAvailableForCompensation)
                .When(state => state.UseForCompensation)
                .WithMessage(state => $"The state with name '{state.Name}' must not be part of the main control flow to be used as a compensation state");
        }

        /// <summary>
        /// Gets the <see cref="WorkflowDefinition"/> to validate
        /// </summary>
        protected WorkflowDefinition Workflow { get; }

        /// <summary>
        /// Determines whether or not the specified <see cref="StateDefinition"/> exists
        /// </summary>
        /// <param name="stateName">The name of the <see cref="StateDefinition"/> to check</param>
        /// <returns>A boolean indicating whether or not the specified <see cref="StateDefinition"/> exists</returns>
        protected virtual bool ReferenceExistingState(string stateName)
        {
            return this.Workflow.TryGetState(stateName, out _);
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

        /// <summary>
        /// Determines whether or not the specified <see cref="StateDefinition"/> exists
        /// </summary>
        /// <param name="transition">The <see cref="TransitionDefinition"/> that references the <see cref="StateDefinition"/> to check</param>
        /// <returns>A boolean indicating whether or not the specified <see cref="StateDefinition"/> exists</returns>
        protected virtual bool ReferenceExistingState(TransitionDefinition transition)
        {
            return this.ReferenceExistingState(transition.To);
        }

        /// <summary>
        /// Determines whether or not the specified <see cref="StateDefinition"/> defines a compensation state
        /// </summary>
        /// <param name="state">The <see cref="StateDefinition"/> to check</param>
        /// <param name="endDefinition">The <see cref="EndDefinition"/> that references the <see cref="StateDefinition"/> to check</param>
        /// <returns>A boolean indicating whether or not the specified <see cref="StateDefinition"/> defines a compensation state</returns>
        protected virtual bool DefineCompensationState(TState state, EndDefinition endDefinition)
        {
            return !string.IsNullOrWhiteSpace(state.CompensatedBy);
        }

        /// <summary>
        /// Determines whether or not the specified <see cref="StateDefinition"/> defines a compensation state
        /// </summary>
        /// <param name="state">The <see cref="StateDefinition"/> to check</param>
        /// <param name="transitionDefinition">The <see cref="TransitionDefinition"/> that references the <see cref="StateDefinition"/> to check</param>
        /// <returns>A boolean indicating whether or not the specified <see cref="StateDefinition"/> defines a compensation state</returns>
        protected virtual bool DefineCompensationState(TState state, TransitionDefinition transitionDefinition)
        {
            return !string.IsNullOrWhiteSpace(state.CompensatedBy);
        }

        /// <summary>
        /// Determines whether or not the specified <see cref="StateDefinition"/> can be used for compensation
        /// </summary>
        /// <param name="state">The <see cref="StateDefinition"/> to check</param>
        /// <param name="useForCompensation">A boolean indicating whether or not the states needs to be compensated. Always true.</param>
        /// <returns>A boolean indicating whether or not the specified <see cref="StateDefinition"/> defines a compensation state</returns>
        protected virtual bool BeAvailableForCompensation(TState state, bool useForCompensation)
        {
            return true;
            //TODO
            //if (useForCompensation && this.Workflow.IsPartOfMainFlow(state))
            //    context.AddFailure($"The state with name '{state.Name}' must not be part of the main control flow to be used as a compensation state");
        }

    }

}
