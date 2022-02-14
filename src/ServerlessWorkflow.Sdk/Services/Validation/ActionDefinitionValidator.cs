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
    /// Represents the service used to validate <see cref="ActionDefinition"/>s
    /// </summary>
    public class ActionDefinitionValidator
        : AbstractValidator<ActionDefinition>
    {

        /// <summary>
        /// Initializes a new <see cref="ActionDefinitionValidator"/>
        /// </summary>
        /// <param name="workflow">The <see cref="WorkflowDefinition"/> the <see cref="ActionDefinition"/>s to validate belong to</param>
        public ActionDefinitionValidator(WorkflowDefinition workflow)
        {
            this.Workflow = workflow;

            this.RuleFor(a => a.Event)
                .NotNull()
                .When(a => a.Function == null && a.Subflow == null)
                .WithErrorCode($"{nameof(ActionDefinition)}.{nameof(ActionDefinition.Event)}");
            this.RuleFor(a => a.Event!)
                .SetValidator(new EventReferenceValidator(this.Workflow))
                .When(a => a.Event != null)
                .WithErrorCode($"{nameof(ActionDefinition)}.{nameof(ActionDefinition.Event)}");

            this.RuleFor(a => a.Function)
                .NotNull()
                .When(a => a.Event == null && a.Subflow == null)
                .WithErrorCode($"{nameof(ActionDefinition)}.{nameof(ActionDefinition.Function)}");
            this.RuleFor(a => a.Function!)
                .SetValidator(new FunctionReferenceValidator(this.Workflow))
                .When(a => a.Function != null)
                .WithErrorCode($"{nameof(ActionDefinition)}.{nameof(ActionDefinition.Function)}");

            this.RuleFor(a => a.Subflow)
                .NotNull()
                .When(a => a.Event == null && a.Function == null)
                .WithErrorCode($"{nameof(ActionDefinition)}.{nameof(ActionDefinition.Subflow)}");
            this.RuleFor(a => a.Subflow!)
                .SetValidator(new SubflowReferenceValidator(this.Workflow))
                .When(a => a.Subflow != null)
                .WithErrorCode($"{nameof(ActionDefinition)}.{nameof(ActionDefinition.Subflow)}");
        }

        /// <summary>
        /// Gets the <see cref="WorkflowDefinition"/> the <see cref="ActionDefinition"/>s to validate belong to
        /// </summary>
        protected WorkflowDefinition Workflow { get; }

    }

}
