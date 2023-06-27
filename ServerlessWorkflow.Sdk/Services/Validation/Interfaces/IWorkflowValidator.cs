namespace ServerlessWorkflow.Sdk.Services.Validation;

/// <summary>
/// Defines the fundamentals of a service used to validate <see cref="WorkflowDefinition"/>s
/// </summary>
public interface IWorkflowValidator
{

    /// <summary>
    /// Validates the specified <see cref="WorkflowDefinition"/>
    /// </summary>
    /// <param name="workflowDefinition">The <see cref="WorkflowDefinition"/> to validate</param>
    /// <param name="validateSchema">A boolean indicating whether or not to validate the schema of the specified <see cref="WorkflowDefinition"/></param>
    /// <param name="validateDsl">A boolean indicating whether or not to validate the DSL of the specified <see cref="WorkflowDefinition"/></param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="IWorkflowValidationResult"/></returns>
    Task<IWorkflowValidationResult> ValidateAsync(WorkflowDefinition workflowDefinition, bool validateSchema = true, bool validateDsl = true, CancellationToken cancellationToken = default);

}
