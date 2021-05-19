using FluentValidation;
using ServerlessWorkflow.Sdk.Models;

namespace ServerlessWorkflow.Sdk.Services.Validation
{
    /// <summary>
    /// Represents the service used to validate <see cref="FunctionDefinition"/>s
    /// </summary>
    public class FunctionDefinitionValidator
        : AbstractValidator<FunctionDefinition>
    {

        /// <summary>
        /// Initializes a new <see cref="FunctionDefinitionValidator"/>
        /// </summary>
        /// <param name="workflow">The <see cref="WorkflowDefinition"/> the <see cref="FunctionDefinition"/>s to validate belong to</param>
        public FunctionDefinitionValidator(WorkflowDefinition workflow)
        {
            this.Workflow = workflow;
            this.RuleFor(f => f.Name)
                .NotEmpty()
                .WithErrorCode($"{nameof(FunctionDefinition)}.{nameof(FunctionDefinition.Name)}");
            this.RuleFor(f => f.Operation)
                .NotEmpty()
                .WithErrorCode($"{nameof(FunctionDefinition)}.{nameof(FunctionDefinition.Operation)}");
        }

        /// <summary>
        /// Gets the <see cref="WorkflowDefinition"/> the <see cref="FunctionDefinition"/>s to validate belong to
        /// </summary>
        protected WorkflowDefinition Workflow { get; }

    }

}
