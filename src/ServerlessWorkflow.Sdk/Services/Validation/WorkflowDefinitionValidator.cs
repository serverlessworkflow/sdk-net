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
using ServerlessWorkflow.Sdk.Models;
using System;
using System.Linq;

namespace ServerlessWorkflow.Sdk.Services.Validation
{

    /// <summary>
    /// Represents the service used to validate <see cref="WorkflowDefinition"/>s
    /// </summary>
    public class WorkflowDefinitionValidator
        : AbstractValidator<WorkflowDefinition>
    {

        /// <summary>
        /// Initializes a new <see cref="WorkflowDefinitionValidator"/>
        /// </summary>
        /// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
        public WorkflowDefinitionValidator(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
            this.RuleFor(w => w.Id)
                .NotEmpty()
                .When(w => string.IsNullOrWhiteSpace(w.Key));
            this.RuleFor(w => w.Key)
                .NotEmpty()
                .When(w => string.IsNullOrWhiteSpace(w.Id));
            this.RuleFor(w => w.Name)
                .NotEmpty();
            this.RuleFor(w => w.Version)
                .NotEmpty();
            this.RuleFor(w => w.ExpressionLanguage)
                .NotEmpty();
            this.RuleFor(w => w.Start)
                .NotNull();
            this.RuleFor(w => w.Start)
                .Must(ReferenceExistingState)
                .When(w => w.Start != null)
                .WithMessage((workflow, start) => $"Failed to find the state with name '{start.StateName}' specified by the workflow's start definition");
            this.RuleFor(w => w.Events)
                .Must(events => events.Select(s => s.Name).Distinct().Count() == events.Count)
                .When(w => w.Functions != null)
                .WithMessage("Duplicate EventDefinition name(s) found");
            this.RuleFor(w => w.Events)
                .SetValidator(new CollectionPropertyValidator<EventDefinition>(this.ServiceProvider))
                .When(w => w.Events != null);
            this.RuleFor(w => w.Functions)
                .Must(functions => functions.Select(s => s.Name).Distinct().Count() == functions.Count)
                .When(w => w.Functions != null)
                .WithMessage("Duplicate FunctionDefinition name(s) found");
            this.RuleFor(w => w.Functions)
                .SetValidator(new CollectionPropertyValidator<FunctionDefinition>(this.ServiceProvider))
                .When(w => w.Functions != null);
            this.RuleFor(w => w.Retries)
                .Must(retries => retries.Select(s => s.Name).Distinct().Count() == retries.Count)
                .When(w => w.Retries != null)
                .WithMessage("Duplicate RetryPolicyDefinition name(s) found");
            this.RuleFor(w => w.Retries)
                .SetValidator(new CollectionPropertyValidator<RetryStrategyDefinition>(this.ServiceProvider))
                .When(w => w.Functions != null);
            this.RuleFor(w => w.Auths)
                .Must(auths => auths.Select(s => s.Name).Distinct().Count() == auths.Count)
                .When(w => w.Auths != null)
                .WithMessage("Duplicate AuthenticationDefinition name(s) found");
            this.RuleFor(w => w.Auths)
                .SetValidator(new CollectionPropertyValidator<AuthenticationDefinition>(this.ServiceProvider))
                .When(w => w.Auths != null);
            this.RuleFor(w => w.States)
                .NotEmpty();
            this.RuleFor(w => w.States)
                .Must(states => states.Select(s => s.Name).Distinct().Count() == states.Count)
                .When(w => w.States != null)
                .WithMessage("Duplicate StateDefinition name(s) found");
            this.RuleFor(w => w.States)
                .SetValidator(new WorkflowStatesPropertyValidator(this.ServiceProvider))
                .When(w => w.States != null);
        }

        /// <summary>
        /// Gets the current <see cref="IServiceProvider"/>
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }

        /// <inheritdoc/>
        public override ValidationResult Validate(ValidationContext<WorkflowDefinition> context)
        {
            ValidationResult validationResult = base.Validate(context);
            if (context.InstanceToValidate.States != null 
                && !context.InstanceToValidate.States.Any(s => s.End != null))
                validationResult.Errors.Add(new ValidationFailure("End", $"The workflow's main control flow must specify an EndDefinition"));
            return validationResult;
        }

        /// <summary>
        /// Determines whether or not the specified <see cref="StartDefinition"/> defines an existing <see cref="StateDefinition"/>
        /// </summary>
        /// <param name="workflow">The <see cref="WorkflowDefinition"/> to validate</param>
        /// <param name="start">The <see cref="StartDefinition"/> to validate</param>
        /// <returns>A boolean indicating whether or not the specified <see cref="StartDefinition"/> defines an existing <see cref="StateDefinition"/></returns>
        protected virtual bool ReferenceExistingState(WorkflowDefinition workflow, StartDefinition start)
        {
            return workflow.TryGetState(start.StateName, out _);
        }

    }

}
