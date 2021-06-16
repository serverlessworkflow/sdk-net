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
    /// Represents the service used to validate <see cref="FunctionDefinition"/>s
    /// </summary>
    public class FunctionDefinitionValidator
        : AbstractValidator<FunctionDefinition>
    {

        /// <summary>
        /// Initializes a new <see cref="FunctionDefinitionValidator"/>
        /// </summary>
        /// <param name="workflow">The <see cref="WorkflowDefinition"/> the <see cref="FunctionDefinition"/>s to validate belong to</param>
        public FunctionDefinitionValidator(WorkflowDefinition workflow)
        {
            this.Workflow = workflow;
            this.RuleFor(f => f.Name)
                .NotEmpty()
                .WithErrorCode($"{nameof(FunctionDefinition)}.{nameof(FunctionDefinition.Name)}");
            this.RuleFor(f => f.Operation)
                .NotEmpty()
                .WithErrorCode($"{nameof(FunctionDefinition)}.{nameof(FunctionDefinition.Operation)}");
        }

        /// <summary>
        /// Gets the <see cref="WorkflowDefinition"/> the <see cref="FunctionDefinition"/>s to validate belong to
        /// </summary>
        protected WorkflowDefinition Workflow { get; }

    }

}
