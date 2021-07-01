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
using Microsoft.Extensions.DependencyInjection;
using ServerlessWorkflow.Sdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServerlessWorkflow.Sdk.Services.Validation
{
    /// <summary>
    /// Represents the service used to validate a workflow's <see cref="RetryStrategyDefinition"/>s
    /// </summary>
    public class CollectionPropertyValidator<T>
        : PropertyValidator
    {

        /// <summary>
        /// Initializes a new <see cref="CollectionPropertyValidator{T}"/>
        /// </summary>
        /// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
        public CollectionPropertyValidator(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// Gets the current <see cref="IServiceProvider"/>
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }

        /// <inheritdoc/>
        protected override bool IsValid(PropertyValidatorContext context)
        {
            IEnumerable<T> elements = (IEnumerable<T>)context.PropertyValue;
            int index = 0;
            foreach (T elem in elements)
            {
                IEnumerable<IValidator<T>> validators = this.ServiceProvider.GetServices<IValidator<T>>();
                foreach (IValidator<T> validator in validators)
                {
                    ValidationResult validationResult = validator.Validate(elem);
                    if (validationResult.IsValid)
                        continue;
                    this.ErrorCode = $"{context.PropertyName}[{index}]";
                    this.SetErrorMessage(string.Join(Environment.NewLine, validationResult.Errors.Select(e => $"{e.ErrorCode}: {e.ErrorMessage}")));
                    return false;
                }
                index++;
            }
            return true;
        }

    }

}
