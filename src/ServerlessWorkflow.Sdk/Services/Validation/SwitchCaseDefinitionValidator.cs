using FluentValidation;
using ServerlessWorkflow.Sdk.Models;

namespace ServerlessWorkflow.Sdk.Services.Validation
{
    /// <summary>
    /// Represents the base class of all services used to validate <see cref="SwitchCaseDefinition"/>s
    /// </summary>
    /// <typeparam name="TCondition">The type of <see cref="SwitchCaseDefinition"/> to validate</typeparam>
    public abstract class SwitchCaseDefinitionValidator<TCondition>
        : AbstractValidator<TCondition>
        where TCondition : SwitchCaseDefinition
    {

        /// <summary>
        /// Initializes a new <see cref="SwitchCaseDefinitionValidator{TCondition}"/>
        /// </summary>
        /// <param name="workflow">The <see cref="WorkflowDefinition"/> the <see cref="SwitchCaseDefinition"/> to validate belongs to</param>
        /// <param name="state">The <see cref="SwitchStateDefinition"/> the <see cref="SwitchCaseDefinition"/> to validate belongs to</param>
        protected SwitchCaseDefinitionValidator(WorkflowDefinition workflow, SwitchStateDefinition state)
        {
            this.Workflow = workflow;
            this.State = state;
            this.RuleFor(c => c.Transition)
                .NotNull()
                .When(c => c.End == null)
                .WithErrorCode($"{nameof(DataCaseDefinition)}.{nameof(DataCaseDefinition.Transition)}")
                .WithMessage($"One of either '{nameof(DataCaseDefinition.Transition)}' or '{nameof(DataCaseDefinition.End)}' properties must be set");
            this.RuleFor(c => c.End)
                .NotNull()
                .When(c => c.Transition == null)
                .WithErrorCode($"{nameof(DataCaseDefinition)}.{nameof(DataCaseDefinition.End)}")
                .WithMessage($"One of either '{nameof(DataCaseDefinition.Transition)}' or '{nameof(DataCaseDefinition.End)}' properties must be set");
        }

        /// <summary>
        /// Gets the <see cref="WorkflowDefinition"/> the <see cref="SwitchCaseDefinition"/> to validate belongs to
        /// </summary>
        protected WorkflowDefinition Workflow { get; }

        /// <summary>
        /// Gets the <see cref="SwitchStateDefinition"/> the <see cref="SwitchCaseDefinition"/> to validate belongs to
        /// </summary>
        protected SwitchStateDefinition State { get; }

    }

}
