using FluentValidation;

namespace ServerlessWorkflow.Sdk.Services.Validation;

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
        this.RuleFor(f => f.RefName)
            .NotEmpty()
            .WithErrorCode($"{nameof(FunctionReference)}.{nameof(FunctionReference.RefName)}");
        this.RuleFor(f => f.RefName)
            .Must(ReferenceExistingFunction)
            .When(f => !string.IsNullOrWhiteSpace(f.RefName))
            .WithErrorCode($"{nameof(FunctionReference)}.{nameof(FunctionReference.RefName)}")
            .WithMessage(f => $"Failed to find a function with name '{f.RefName}'");
        this.RuleFor(f => f.SelectionSet)
            .Empty()
            .When(DoesNotReferenceGraphQLFunction);
        this.RuleFor(f => f.SelectionSet)
            .NotEmpty()
            .When(ReferencesGraphQLFunction);
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
    protected virtual bool ReferenceExistingFunction(string functionName) => this.Workflow.TryGetFunction(functionName, out _);

    /// <summary>
    /// Determines whether or not the specified <see cref="FunctionReference"/> references a <see cref="FunctionDefinition"/> of type '<see cref="FunctionType.GraphQL"/>'
    /// </summary>
    /// <param name="functionReference">The <see cref="FunctionReference"/> to validate</param>
    /// <returns>A boolean indicating whether or not the referenced <see cref="FunctionDefinition"/> is not of '<see cref="FunctionType.GraphQL"/>' type</returns>
    protected virtual bool DoesNotReferenceGraphQLFunction(FunctionReference functionReference)
    {
        if (string.IsNullOrWhiteSpace(functionReference.RefName)) return false;
        if (!this.Workflow.TryGetFunction(functionReference.RefName, out FunctionDefinition function)) return false;
        return function.Type != FunctionType.GraphQL;
    }

    /// <summary>
    /// Determines whether or not the specified <see cref="FunctionReference"/> references a <see cref="FunctionDefinition"/> of type '<see cref="FunctionType.GraphQL"/>'
    /// </summary>
    /// <param name="functionReference">The <see cref="FunctionReference"/> to validate</param>
    /// <returns>A boolean indicating whether or not the referenced <see cref="FunctionDefinition"/> is of '<see cref="FunctionType.GraphQL"/>' type</returns>
    protected virtual bool ReferencesGraphQLFunction(FunctionReference functionReference)
    {
        if (string.IsNullOrWhiteSpace(functionReference.RefName)) return false;
        if (!this.Workflow.TryGetFunction(functionReference.RefName, out FunctionDefinition function)) return false;
        return function.Type == FunctionType.GraphQL;
    }

}
