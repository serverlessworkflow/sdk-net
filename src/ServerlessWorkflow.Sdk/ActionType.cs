namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Enumerates all types of actions
/// </summary>
public static class ActionType
{

    /// <summary>
    /// Indicates an action that invokes a function
    /// </summary>
    public const string Function = "function";

    /// <summary>
    /// Indicates an action that executes a cloud event trigger
    /// </summary>
    public const string Event = "event";

    /// <summary>
    /// Indicates an action that executes a subflow
    /// </summary>
    public const string Subflow = "subflow";

}
