namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines the fundamentals of a runtime error
/// </summary>
public interface IRuntimeError
{

    /// <summary>
    /// Gets/sets an uri that reference the type of the described problem.
    /// </summary>
    Uri Type { get; }

    /// <summary>
    /// Gets/sets a short, human-readable summary of the problem type.It SHOULD NOT change from occurrence to occurrence of the problem, except for purposes of localization.
    /// </summary>
    string Title { get; }

    /// <summary>
    /// Gets/sets the status code produced by the described problem
    /// </summary>
    ushort Status { get; }

    /// <summary>
    /// Gets/sets a human-readable explanation specific to this occurrence of the problem.
    /// </summary>
    string? Detail { get; }

    /// <summary>
    /// Gets/sets a <see cref="Uri"/> reference that identifies the specific occurrence of the problem. It may or may not yield further information if dereferenced.
    /// </summary>
    Uri? Instance { get; }

    /// <summary>
    /// Gets/sets a mapping containing problem details extension data, if any
    /// </summary>
    IDictionary<string, JsonElement>? ExtensionData { get; }

}
