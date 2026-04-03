namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents the default implementation of the <see cref="ITaskExecutorFactory"/> interface.
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="registry">The <see cref="TaskExecutorRegistry"/> used to resolve executor types</param>
public sealed class TaskExecutorFactory(IServiceProvider serviceProvider, TaskExecutorRegistry registry)
    : ITaskExecutorFactory
{

    /// <inheritdoc/>
    public ITaskExecutor Create(ITaskExecutionContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        var executorType = registry.Resolve(context.Definition.Type) ?? throw new InvalidOperationException($"No task executor registered for task definition type '{context.Definition.GetType().Name}'");
        return (ITaskExecutor)ActivatorUtilities.CreateInstance(serviceProvider, executorType, context);
    }

    /// <inheritdoc/>
    public ITaskExecutor<TDefinition> Create<TDefinition>(ITaskExecutionContext<TDefinition> context)
        where TDefinition : TaskDefinition
    {
        ArgumentNullException.ThrowIfNull(context);
        var executorType = registry.Resolve(context.Definition.Type) ?? throw new InvalidOperationException($"No task executor registered for task type '{context.Definition.Type}'");
        return (ITaskExecutor<TDefinition>)ActivatorUtilities.CreateInstance(serviceProvider, executorType, context);
    }

}
