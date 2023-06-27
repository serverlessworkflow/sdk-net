namespace ServerlessWorkflow.Sdk.Services.Validation;

/// <summary>
/// Defines the fundamentals of an object used to describe a <see cref="WorkflowDefinition"/>'s validation results
/// </summary>
public interface IWorkflowValidationResult
{

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> containing the schema-related validation errors that have occured during the <see cref="WorkflowDefinition"/>'s validation
    /// </summary>
    IEnumerable<KeyValuePair<string, string>>? SchemaValidationErrors { get; }

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> containing the DSL-related validation errors that have occured during the <see cref="WorkflowDefinition"/>'s validation
    /// </summary>
    IEnumerable<KeyValuePair<string, string>>? DslValidationErrors { get; }

    /// <summary>
    /// Gets a boolean indicating whether or not the <see cref="WorkflowDefinition"/> is valid
    /// </summary>
    bool IsValid { get; }

}
