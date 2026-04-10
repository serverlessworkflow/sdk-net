#pragma warning disable IDE0130 // Namespace does not match folder structure

namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines extensions for <see cref="ITaskExecutionContext"/>s
/// </summary>
public static class ITaskExecutionContextExtensions
{

    /// <summary>
    /// Gets a new <see cref="TaskDescriptor"/> used to describe the <see cref="ITaskExecutionContext"/>
    /// </summary>
    /// <param name="task">The <see cref="ITaskExecutionContext"/> to describe</param>
    /// <returns>A new <see cref="TaskDescriptor"/></returns>
    public static TaskDescriptor GetDescriptor(this ITaskExecutionContext task)
    {
        return new()
        {
            Id = task.State.Id,
            Name = task.State.Name,
            Reference = task.State.Reference,
            Definition = task.Definition,
            Input = task.State.Input,
            Output = task.State.Output,
            StartedAt = task.State.StartedAt?.GetDescriptor()
        };
    }

}
