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
    internal class FunctionDefinitionValidator
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
            this.RuleFor(f => f.AuthRef!)
                .Must(ReferenceExistingAuthentication)
                .WithErrorCode($"{nameof(FunctionDefinition)}.{nameof(FunctionDefinition.AuthRef)}")
                .WithMessage(f => $"Failed to find an authentication definition with name '{f.AuthRef}'")
                .When(f => !string.IsNullOrWhiteSpace(f.AuthRef));
        }

        /// <summary>
        /// Gets the <see cref="WorkflowDefinition"/> the <see cref="FunctionDefinition"/>s to validate belong to
        /// </summary>
        protected WorkflowDefinition Workflow { get; }

        /// <summary>
        /// Determines whether or not the specified <see cref="AuthenticationDefinition"/> exists
        /// </summary>
        /// <param name="authenticationName">The name of the <see cref="AuthenticationDefinition"/> to check</param>
        /// <returns>A boolean indicating whether or not the specified <see cref="AuthenticationDefinition"/> exists</returns>
        protected virtual bool ReferenceExistingAuthentication(string authenticationName)
        {
            return this.Workflow.TryGetAuthentication(authenticationName, out _);
        }

    }

}
