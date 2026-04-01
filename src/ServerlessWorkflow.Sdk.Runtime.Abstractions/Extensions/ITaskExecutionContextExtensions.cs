#pragma warning disable IDE0130 // Namespace does not match folder structure

namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines extensions for <see cref="ITaskExecutionContext"/>s.
/// </summary>
public static class ITaskExecutionContextExtensions
{

    /// <summary>
    /// Gets an object describing the current task execution context
    /// </summary>
    /// <param name="task">The <see cref="ITaskExecutionContext"/> to get the descriptor of</param>
    /// <returns>A new <see cref="TaskDescriptor"/> describing the current task execution context</returns>
    public static TaskDescriptor GetDescriptor(this ITaskExecutionContext task) => new()
    {
        Name = task.Instance.State.Name,
        Definition = task.Definition,
        Reference = task.Instance.State.Reference,
        Input = task.Instance.State.Input,
        Output = task.Instance.State.Output,
        StartedAt = task.Instance.State.StartedAt?.GetDescriptor()
    };

}