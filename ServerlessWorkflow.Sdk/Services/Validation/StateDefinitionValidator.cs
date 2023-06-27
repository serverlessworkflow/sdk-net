using FluentValidation;

namespace ServerlessWorkflow.Sdk.Services.Validation;

/// <summary>
/// Represents the base class for all <see cref="IValidator"/>s used to validate state definitions
/// </summary>
/// <typeparam name="TState">The type of state definition to validate</typeparam>
internal abstract class StateDefinitionValidator<TState>
    : AbstractValidator<TState>
    where TState : StateDefinition
{

    /// <summary>
    /// Initializes a new <see cref="StateDefinitionValidator{TState}"/>
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> to validate</param>
    protected StateDefinitionValidator(WorkflowDefinition workflow)
    {
        this.Workflow = workflow;
        this.RuleFor(s => s.Name)
            .NotNull();
        this.RuleFor(s => s.CompensatedBy!)
            .Must(ReferenceExistingState)
            .When(s => !string.IsNullOrWhiteSpace(s.CompensatedBy))
            .WithMessage((state, stateName) => $"Failed to find the state '{stateName}' to use for compensation");
        this.RuleFor(s => s.Transition!)
            .Must(ReferenceExistingState)
            .When(s => s.Transition != null)
            .WithMessage((state, stateName) => $"Failed to find the state '{stateName}' to transition to");
        this.RuleFor(s => s.Transition!)
            .Must(DefineCompensationState)
            .When(s => s.Transition != null && s.Transition.Compensate)
            .WithMessage(state => $"The '{nameof(StateDefinition.CompensatedBy)}' property of the state '{state.Name}' must be set when enabling its compensation (in both Transition and End definitions)");
        this.RuleFor(s => s.End!)
            .Must(DefineCompensationState)
            .When(s => s.End != null && s.End.Compensate)
            .WithMessage(state => $"The '{nameof(StateDefinition.CompensatedBy)}' property of the state '{state.Name}' must be set when enabling its compensation (in both Transition and End definitions)");
        this.RuleForEach(s => s.Errors)
            .SetValidator((s, e) => new ErrorHandlerDefinitionValidator(this.Workflow, s));
        this.RuleFor(s => s.UsedForCompensation)
            .Must(BeAvailableForCompensation)
            .When(state => state.UsedForCompensation)
            .WithMessage(state => $"The state with name '{state.Name}' must not be part of the main control flow to be used as a compensation state");
    }

    /// <summary>
    /// Gets the <see cref="WorkflowDefinition"/> to validate
    /// </summary>
    protected WorkflowDefinition Workflow { get; }

    /// <summary>
    /// Determines whether or not the specified state definition exists
    /// </summary>
    /// <param name="transition">The name of the state definition to check</param>
    /// <returns>A boolean indicating whether or not the specified state definition exists</returns>
    protected virtual bool ReferenceExistingState(TransitionDefinition transition)
    {
        return this.Workflow.TryGetState(transition.NextState, out _);
    }

    /// <summary>
    /// Determines whether or not the specified state definition exists
    /// </summary>
    /// <param name="stateName">The name of the state definition to check</param>
    /// <returns>A boolean indicating whether or not the specified state definition exists</returns>
    protected virtual bool ReferenceExistingState(string stateName)
    {
        return this.Workflow.TryGetState(stateName, out _);
    }

    /// <summary>
    /// Determines whether or not the specified <see cref="EventDefinition"/> exists
    /// </summary>
    /// <param name="eventName">The name of the <see cref="EventDefinition"/> to check</param>
    /// <returns>A boolean indicating whether or not the specified <see cref="EventDefinition"/> exists</returns>
    protected virtual bool ReferenceExistingEvent(string eventName)
    {
        return this.Workflow.TryGetEvent(eventName, out _);
    }

    /// <summary>
    /// Determines whether or not the specified state definition defines a compensation state
    /// </summary>
    /// <param name="state">The state definition to check</param>
    /// <param name="oneOf">The <see cref="TransitionDefinition"/> that references the state definition to check</param>
    /// <returns>A boolean indicating whether or not the specified state definition defines a compensation state</returns>
    protected virtual bool DefineCompensationState(TState state, TransitionDefinition oneOf)
    {
        return !string.IsNullOrWhiteSpace(state.CompensatedBy);
    }

    /// <summary>
    /// Determines whether or not the specified state definition defines a compensation state
    /// </summary>
    /// <param name="state">The state definition to check</param>
    /// <param name="oneOf">The <see cref="EndDefinition"/> that references the state definition to check</param>
    /// <returns>A boolean indicating whether or not the specified state definition defines a compensation state</returns>
    protected virtual bool DefineCompensationState(TState state, EndDefinition oneOf)
    {
        return !string.IsNullOrWhiteSpace(state.CompensatedBy);
    }

    /// <summary>
    /// Determines whether or not the specified state definition can be used for compensation
    /// </summary>
    /// <param name="state">The state definition to check</param>
    /// <param name="useForCompensation">A boolean indicating whether or not the states needs to be compensated. Always true.</param>
    /// <returns>A boolean indicating whether or not the specified state definition defines a compensation state</returns>
    protected virtual bool BeAvailableForCompensation(TState state, bool useForCompensation)
    {
        return true;
        //TODO
        //if (useForCompensation && this.Workflow.IsPartOfMainFlow(state))
        //    context.AddFailure($"The state with name '{state.Name}' must not be part of the main control flow to be used as a compensation state");
    }

}
