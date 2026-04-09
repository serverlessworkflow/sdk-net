#pragma warning disable IDE0130 // Namespace does not match folder structure

namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines extensions for <see cref="ITaskInstance"/>s.
/// </summary>
public static class ITaskInstanceExtensions
{

    /// <summary>
    /// Gets an object describing the <see cref="ITaskInstance"/>
    /// </summary>
    /// <param name="task">The <see cref="ITaskInstance"/> to get the descriptor of</param>
    /// <returns>A new <see cref="TaskDescriptor"/> describing the current task execution context</returns>
    public static TaskDescriptor GetDescriptor(this ITaskInstance task) => new()
    {
        Name = task.State.Name,
        Definition = task.Definition,
        Reference = task.State.Reference,
        Input = task.State.Input,
        Output = task.State.Output,
        StartedAt = task.State.StartedAt?.GetDescriptor()
    };

}