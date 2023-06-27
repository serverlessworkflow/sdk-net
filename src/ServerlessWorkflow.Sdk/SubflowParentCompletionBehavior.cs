namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Enumerates the ways a subflow should behave when its parent completes before it 
/// </summary>
public static class SubflowParentCompletionBehavior
{

    /// <summary>
    /// Indicates that the subflow is terminated upon completion of its parent
    /// </summary>
    public const string Terminate = "terminate";
    /// <summary>
    /// Indicates that the subflow should continue to run even if its parent has completed
    /// </summary>
    public const string Continue = "continue";

}
