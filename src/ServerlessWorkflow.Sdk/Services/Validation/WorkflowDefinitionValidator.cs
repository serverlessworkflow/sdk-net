using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
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

    public abstract class StateDefinitionValidator<TState>
        : AbstractValidator<TState>
        where TState : StateDefinition
    {

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
                    .WithMessage(state => $"The '{nameof(StateDefinition.CompensatedBy)}' property of the state '{state.Name}' must be set when enabling its compensation (in both Transition and End definitions)"); ;
            this.RuleFor(s => s.End)
                .Must(DefineCompensationState)
                    .When(s => s.End != null && s.End.Compensate)
                    .WithMessage(state => $"The '{nameof(StateDefinition.CompensatedBy)}' property of the state '{state.Name}' must be set when enabling its compensation (in both Transition and End definitions)");
            this.RuleForEach(s => s.Errors)
                .SetValidator((s, e) => new ErrorHandlerDefinitionValidator(this.Workflow, s));
            this.RuleFor(s => s.UseForCompensation)
                .Custom(CanBeUsedForCompensation);
        }

        protected WorkflowDefinition Workflow { get; }

        protected virtual bool ReferenceExistingState(string stateName)
        {
            return this.Workflow.TryGetState(stateName, out _);
        }

        protected virtual bool ReferenceExistingState(TransitionDefinition transition)
        {
            return this.ReferenceExistingState(transition.To);
        }

        protected virtual bool DefineCompensationState(TState state, EndDefinition endDefinition)
        {
            return !string.IsNullOrWhiteSpace(state.CompensatedBy);
        }

        protected virtual bool DefineCompensationState(TState state, TransitionDefinition transitionDefinition)
        {
            return !string.IsNullOrWhiteSpace(state.CompensatedBy);
        }

        protected virtual void MustReferenceExistingFunction(string functionName, CustomContext context)
        {

        }

        protected virtual void MustReferenceExistingEvent(string eventName, CustomContext context)
        {

        }

        protected virtual void ReferenceExistingState(string stateName, CustomContext context)
        {
            if (string.IsNullOrWhiteSpace(stateName))
                return;
            if (!this.Workflow.TryGetState(stateName, out _))
                context.AddFailure($"Failed to find the state with name '{stateName}'");
        }

        protected virtual void ReferenceExistingState(TransitionDefinition transition, CustomContext context)
        {
            this.ReferenceExistingState(transition.To, context);
        }

        protected virtual void CanBeUsedForCompensation(bool useForCompensation, CustomContext context)
        {
            TState state = (TState)context.ParentContext.InstanceToValidate;
            //TODO
            //if (useForCompensation && this.Workflow.IsPartOfMainFlow(state))
            //    context.AddFailure($"The state with name '{state.Name}' must not be part of the main control flow to be used as a compensation state");
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

    public class CallbacKStateValidator
        : StateDefinitionValidator<CallbackStateDefinition>
    {

        public CallbacKStateValidator(WorkflowDefinition workflow) 
            : base(workflow)
        {
            this.RuleFor(s => s.Action)
                .NotNull()
                .SetValidator(new ActionDefinitionValidation(workflow));
            this.RuleFor(s => s.Event)
                .NotNull()
                .Custom(MustReferenceExistingEvent);
        }

    }

    public class ActionDefinitionValidation
        : AbstractValidator<ActionDefinition>
    {

        public ActionDefinitionValidation(WorkflowDefinition workflow)
        {
            this.Workflow = workflow;
        }

        protected WorkflowDefinition Workflow { get; }

    }

}
