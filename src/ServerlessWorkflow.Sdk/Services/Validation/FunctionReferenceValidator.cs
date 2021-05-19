using FluentValidation;
using ServerlessWorkflow.Sdk.Models;

namespace ServerlessWorkflow.Sdk.Services.Validation
{
    /// <summary>
    /// Represents the service used to validate <see cref="FunctionReference"/>s
    /// </summary>
    public class FunctionReferenceValidator
        : AbstractValidator<FunctionReference>
    {

        /// <summary>
        /// Initializes a new <see cref="FunctionReferenceValidator"/>
        /// </summary>
        /// <param name="workflow">The <see cref="WorkflowDefinition"/> the <see cref="FunctionReference"/>s to validate belong to</param>
        public FunctionReferenceValidator(WorkflowDefinition workflow)
        {
            this.Workflow = workflow;
            this.RuleFor(f => f.Name)
                .NotEmpty()
                .WithErrorCode($"{nameof(FunctionReference)}.{nameof(FunctionReference.Name)}");
            this.RuleFor(f => f.Name)
                .Must(ReferenceExistingFunction)
                .When(f => !string.IsNullOrWhiteSpace(f.Name))
                .WithErrorCode($"{nameof(FunctionReference)}.{nameof(FunctionReference.Name)}")
                .WithMessage(f => $"Failed to find a function with name '{f.Name}'");
        }

        /// <summary>
        /// Gets the <see cref="WorkflowDefinition"/> the <see cref="FunctionReference"/>s to validate belong to
        /// </summary>
        protected WorkflowDefinition Workflow { get; }

        /// <summary>
        /// Determines whether or not the specified <see cref="FunctionDefinition"/> exists
        /// </summary>
        /// <param name="functionName">The name of the <see cref="FunctionDefinition"/> to check</param>
        /// <returns>A boolean indicating whether or not the specified <see cref="FunctionDefinition"/> exists</returns>
        protected virtual bool ReferenceExistingFunction(string functionName)
        {
            return this.Workflow.TryGetFunction(functionName, out _);
        }

    }

}
