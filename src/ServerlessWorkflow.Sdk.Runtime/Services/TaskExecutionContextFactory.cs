namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents the default implementation of the <see cref="ITaskExecutionContextFactory"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
public sealed class TaskExecutionContextFactory(IServiceProvider serviceProvider)
    : ITaskExecutionContextFactory
{

    /// <inheritdoc/>
    public ITaskExecutionContext Create(IWorkflowExecutionContext workflow, TaskDefinition definition, ITaskInstance state, JsonObject? arguments = null)
    {
        ArgumentNullException.ThrowIfNull(workflow);
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(definition);
        var contextType = typeof(TaskExecutionContext<>).MakeGenericType(definition.GetType());
        return (ITaskExecutionContext)ActivatorUtilities.CreateInstance(serviceProvider, contextType, workflow, definition, state, arguments ?? new JsonObject());
    }

}