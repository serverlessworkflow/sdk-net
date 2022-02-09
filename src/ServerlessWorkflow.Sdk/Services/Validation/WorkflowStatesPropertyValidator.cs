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
using System.Linq;
using System.Reflection;

namespace ServerlessWorkflow.Sdk.Services.Validation
{

    /// <summary>
    /// Represents the service used to validate a workflow's <see cref="StateDefinition"/>s
    /// </summary>
    public class WorkflowStatesPropertyValidator
        : PropertyValidator<WorkflowDefinition, List<StateDefinition>>
    {

        private static Dictionary<Type, IEnumerable<Type>> StateValidatorTypes = typeof(WorkflowDefinitionValidator).Assembly.GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface && !t.IsGenericType && t.IsClass && t.GetGenericType(typeof(StateDefinitionValidator<>)) != null)
            .GroupBy(t => t.GetGenericType(typeof(StateDefinitionValidator<>)).GetGenericArguments().First())
            .ToDictionary(g => g.Key, g => g.AsEnumerable());

        /// <summary>
        /// Initializes a new <see cref="WorkflowStatesPropertyValidator"/>
        /// </summary>
        /// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
        public WorkflowStatesPropertyValidator(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        public override string Name => throw new NotImplementedException();

        /// <summary>
        /// Gets the current <see cref="IServiceProvider"/>
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }

        /// <inheritdoc/>
        public override bool IsValid(ValidationContext<WorkflowDefinition> context, List<StateDefinition> value)
        {
            int index = 0;
            foreach (StateDefinition state in value)
            {
                if (!StateValidatorTypes.TryGetValue(state.GetType(), out IEnumerable<Type> validatorTypes))
                    continue;
                IEnumerable<IValidator> validators = validatorTypes.Select(t => (IValidator)Activator.CreateInstance(t, (WorkflowDefinition)context.InstanceToValidate));
                foreach (IValidator validator in validators)
                {
                    object[] args = new object[] { state };
                    MethodInfo validationMethod = typeof(IValidator<>).MakeGenericType(state.GetType()).GetMethods().Single(m => m.Name == nameof(IValidator.Validate) && m.GetParameters().Length == 1 && m.GetParameters().First().ParameterType != typeof(IValidationContext));
                    ValidationResult validationResult = (ValidationResult)validationMethod.Invoke(validator, args);
                    if (validationResult.IsValid)
                        continue;
                    foreach (var failure in validationResult.Errors)
                    {
                        context.AddFailure(failure);
                    }
                    return false;
                }
                index++;
            }
            return true;
        }

    }

}
