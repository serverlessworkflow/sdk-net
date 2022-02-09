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
using FluentValidation.Results;
using FluentValidation.Validators;
using ServerlessWorkflow.Sdk.Models;
using System;
using System.Collections.Generic;

namespace ServerlessWorkflow.Sdk.Services.Validation
{
    /// <summary>
    /// Represents the <see cref="PropertyValidator"/> used to validate a <see cref="FunctionDefinition"/> collection
    /// </summary>
    public class FunctionDefinitionCollectionValidator
        : PropertyValidator<WorkflowDefinition, IEnumerable<FunctionDefinition>>
    {

        /// <inheritdoc/>
        public override string Name => throw new NotImplementedException();

        /// <inheritdoc/>
        public override bool IsValid(ValidationContext<WorkflowDefinition> context, IEnumerable<FunctionDefinition> value)
        {
            WorkflowDefinition workflow = context.InstanceToValidate;
            int index = 0;
            IValidator<FunctionDefinition> validator = new FunctionDefinitionValidator(workflow);
            foreach (FunctionDefinition function in value)
            {

                ValidationResult validationResult = validator.Validate(function);
                if (validationResult.IsValid)
                {
                    index++;
                    continue;
                }
                foreach(var failure in validationResult.Errors)
                {
                    context.AddFailure(failure);
                }
                return false;
            }
            return true;
        }

    }

}
