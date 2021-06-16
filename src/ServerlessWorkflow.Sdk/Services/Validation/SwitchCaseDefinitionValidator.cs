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

    }

}
