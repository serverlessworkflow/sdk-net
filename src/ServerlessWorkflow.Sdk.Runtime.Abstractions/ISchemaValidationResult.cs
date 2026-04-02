namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines the fundamentals of a schema validation result
/// </summary>
public interface ISchemaValidationResult
{

    /// <summary>
    /// Gets a boolean indicating whether or not the validation result is valid
    /// </summary>
    bool IsValid { get; }

    /// <summary>
    /// Gets a mapping of errors, if any, that occurred during the validation process. The keys of the mapping represent the paths to the invalid nodes, while the values are lists of error messages related to each path.
    /// </summary>
    IReadOnlyDictionary<string, IReadOnlyList<string>>? Errors { get; }

}