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
                .NotEmpty();
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
            this.RuleFor(w => w.Functions)
                .Must(functions => functions.Select(s => s.Name).Distinct().Count() == functions.Count)
                    .When(w => w.Functions != null)
                    .WithMessage("Duplicate FunctionDefinition name(s) found");
            this.RuleFor(w => w.Retries)
                .Must(retries => retries.Select(s => s.Name).Distinct().Count() == retries.Count)
                    .When(w => w.Retries != null)
                    .WithMessage("Duplicate RetryPolicyDefinition name(s) found");
            this.RuleFor(w => w.States)
                .NotEmpty();
            this.RuleFor(w => w.States)
                .Must(states => states.Select(s => s.Name).Distinct().Count() == states.Count)
                .When(w => w.States != null)
                .WithMessage("Duplicate StateDefinition name(s) found");
            this.RuleFor(w => w.States)
                .SetValidator(new WorkflowStatesValidator(this.ServiceProvider))
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
            return workflow.TryGetState(start.StateName, out StateDefinition x);
        }

    }

    public class ErrorHandlerDefinitionValidator
        : AbstractValidator<ErrorHandlerDefinition>
    {

        public ErrorHandlerDefinitionValidator(WorkflowDefinition workflow, StateDefinition state)
        {
            this.Workflow = workflow;
            this.State = state;
            this.RuleFor(h => h.Error)
                .NotEmpty();
            this.RuleFor(h => h.Code)
                .Empty()
                .When(h => h.Error == "*")
                .WithMessage("The 'Code' property cannot be set when the 'Error' property has been set to '*'");
            this.RuleFor(h => h.End)
                .NotNull()
                .When(h => h.Transition == null);
            this.RuleFor(h => h.Transition)
                .NotNull()
                    .When(h => h.End == null)
                .SetValidator(new TransitionDefinitionValidator(workflow));
        }

        protected WorkflowDefinition Workflow { get; }

        protected StateDefinition State { get; }

    }

    public class TransitionDefinitionValidator
        : AbstractValidator<TransitionDefinition>
    {

        public TransitionDefinitionValidator(WorkflowDefinition workflow)
        {
            this.Workflow = workflow;
            this.RuleFor(t => t.To)
                .NotEmpty();
        }

        protected WorkflowDefinition Workflow { get; }

    }

}
