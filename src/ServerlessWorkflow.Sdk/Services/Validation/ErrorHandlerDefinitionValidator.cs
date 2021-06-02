using FluentValidation;
using ServerlessWorkflow.Sdk.Models;

namespace ServerlessWorkflow.Sdk.Services.Validation
{
    /// <summary>
    /// Represents the service used to validate <see cref="ErrorHandlerDefinition"/>s
    /// </summary>
    public class ErrorHandlerDefinitionValidator
        : AbstractValidator<ErrorHandlerDefinition>
    {

        /// <summary>
        /// Initializes a new <see cref="ErrorHandlerDefinitionValidator"/>
        /// </summary>
        /// <param name="workflow">The <see cref="WorkflowDefinition"/> the <see cref="ErrorHandlerDefinition"/>s to validate belong to</param>
        /// <param name="state">The <see cref="StateDefinition"/> the <see cref="ErrorHandlerDefinition"/>s to validate belong to</param>
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

        /// <summary>
        /// Gets the <see cref="WorkflowDefinition"/> the <see cref="ErrorHandlerDefinition"/>s to validate belong to
        /// </summary>
        protected WorkflowDefinition Workflow { get; }

        /// <summary>
        /// Gets the <see cref="StateDefinition"/> the <see cref="ErrorHandlerDefinition"/>s to validate belong to
        /// </summary>
        protected StateDefinition State { get; }

    }

}
