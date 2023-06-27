namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Enumerates all parallel completion types
/// </summary>
public static class ParallelCompletionType
{

    /// <summary>
    /// Indicates that all branches should be completed before completing the parallel execution
    /// </summary>
    public const string AllOf = "allOf";

    /// <summary>
    /// Indicates that 'N' amount of branches should complete before completing the parallel execution, thus potentially cancelling running branches
    /// </summary>
    public const string AtLeastN = "atLeast";

}
