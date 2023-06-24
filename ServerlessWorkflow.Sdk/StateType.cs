namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Enumerates all types of states
/// </summary>
public static class StateType
{

    /// <summary>
    /// Indicates an operation state
    /// </summary>
    public const string Operation = "operation";

    /// <summary>
    /// Indicates a sleep state
    /// </summary>
    public const string Sleep = "sleep";

    /// <summary>
    /// Indicates an event state
    /// </summary>
    public const string Event = "event";

    /// <summary>
    /// Indicates a parallel state
    /// </summary>
    public const string Parallel = "parallel";

    /// <summary>
    /// Indicates a switch state
    /// </summary>
    public const string Switch = "switch";

    /// <summary>
    /// Indicates an inject state
    /// </summary>
    public const string Inject = "inject";

    /// <summary>
    /// Indicates a foreach state
    /// </summary>
    public const string ForEach = "foreach";

    /// <summary>
    /// Indicates a callback state
    /// </summary>
    public const string Callback = "callback";

    /// <summary>
    /// Gets all supported values
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing all supported values</returns>
    public static IEnumerable<string> GetValues()
    {
        yield return Operation;
        yield return Sleep;
        yield return Event;
        yield return Parallel;
        yield return Switch;
        yield return Inject;
        yield return ForEach;
        yield return Callback;
    }

}
