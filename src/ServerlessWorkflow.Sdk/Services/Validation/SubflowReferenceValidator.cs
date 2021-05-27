using FluentValidation;
using ServerlessWorkflow.Sdk.Models;

namespace ServerlessWorkflow.Sdk.Services.Validation
{
    /// <summary>
    /// Represents the service used to validate <see cref="SubflowReference"/>s
    /// </summary>
    public class SubflowReferenceValidator
        : AbstractValidator<SubflowReference>
    {

        /// <summary>
        /// Initializes a new <see cref="SubflowReferenceValidator"/>
        /// </summary>
        /// <param name="workflow">The <see cref="WorkflowDefinition"/> the <see cref="SubflowReference"/>s to validate belong to</param>
        public SubflowReferenceValidator(WorkflowDefinition workflow)
        {
            this.Workflow = workflow;
            this.RuleFor(w => w.WorkflowId)
                .NotEmpty()
                .WithErrorCode($"{nameof(SubflowReference)}.{nameof(SubflowReference.WorkflowId)}");
        }

        /// <summary>
        /// Gets the <see cref="WorkflowDefinition"/> the <see cref="FunctionReference"/>s to validate belong to
        /// </summary>
        protected WorkflowDefinition Workflow { get; }

    }

}
