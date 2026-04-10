#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines extensions for <see cref="WorkflowDefinition"/>s
/// </summary>
public static class WorkflowDefinitionExtensions
{

    /// <summary>
    /// Gets the next <see cref="TaskDefinition"/> to perform next, if any
    /// </summary>
    /// <param name="workflow">The extended <see cref="WorkflowDefinition"/></param>
    /// <param name="after">The <see cref="ITaskState"/> to perform the next <see cref="ITaskState"/> after</param>
    /// <returns>The next <see cref="TaskDefinition"/> to perform next, if any</returns>
    public static MapEntry<string, TaskDefinition>? GetTaskAfter(this WorkflowDefinition workflow, ITaskState after)
    {
        ArgumentNullException.ThrowIfNull(after);
        switch (after.Status == TaskStatus.Skipped ? FlowDirective.Continue : after.Next)
        {
            case FlowDirective.Continue:
                var afterIndex = workflow.Do.Select(kvp => kvp.Key).ToList().IndexOf(after.Name!);
                return workflow.Do.Skip(afterIndex + 1).FirstOrDefault();
            case FlowDirective.End: case FlowDirective.Exit: return default;
            default: return new(after.Next!, workflow.Do[after.Next!]);
        }
    }

    /// <summary>
    /// Attempts to get the next <see cref="TaskDefinition"/> to perform next, if any
    /// </summary>
    /// <param name="workflow">The extended <see cref="WorkflowDefinition"/></param>
    /// <param name="after">The <see cref="ITaskState"/> to perform the next <see cref="ITaskState"/> after</param>
    /// <param name="task">The next <see cref="TaskDefinition"/> to perform next, if any</param>
    /// <returns>A boolean indicating whether or not a next <see cref="ITaskState"/> must be executed next</returns>
    public static bool TryGetTaskAfter(this WorkflowDefinition workflow, ITaskState after, out MapEntry<string, TaskDefinition> task)
    {
        ArgumentNullException.ThrowIfNull(after);
        task = workflow.GetTaskAfter(after)!;
        return task != null;
    }

}