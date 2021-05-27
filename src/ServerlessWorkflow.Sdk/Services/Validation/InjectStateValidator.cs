using FluentValidation;
using ServerlessWorkflow.Sdk.Models;

namespace ServerlessWorkflow.Sdk.Services.Validation
{
    /// <summary>
    /// Represents a service used to validate <see cref="InjectStateDefinition"/>s
    /// </summary>
    public class InjectStateValidator
        : StateDefinitionValidator<InjectStateDefinition>
    {

        /// <summary>
        /// Initializes a new <see cref="InjectStateValidator"/>
        /// </summary>
        /// <param name="workflow">The <see cref="WorkflowDefinition"/> to validate</param>
        public InjectStateValidator(WorkflowDefinition workflow)
            : base(workflow)
        {
            this.RuleFor(s => s.Data)
                .NotNull()
                .WithErrorCode($"{nameof(InjectStateDefinition)}.{nameof(InjectStateDefinition.Data)}");
        }

    }

}
