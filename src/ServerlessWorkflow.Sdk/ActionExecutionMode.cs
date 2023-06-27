namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Enumerates all types of actions
/// </summary>
public static class ActionExecutionMode
{

    /// <summary>
    /// Indicates a sequential execution of actions
    /// </summary>
    public const string Sequential = "sequential";

    /// <summary>
    /// Indicates a parallel execution of actions
    /// </summary>
    public const string Parallel = "parallel";

}
