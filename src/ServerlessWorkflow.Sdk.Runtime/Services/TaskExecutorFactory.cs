namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents the default implementation of the <see cref="ITaskExecutorFactory"/> interface.
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="registry">The <see cref="TaskExecutorRegistry"/> used to resolve executor types</param>
/// <param name="callRegistry">The <see cref="CallTaskExecutorRegistry"/> used to resolve call-type-specific executor types</param>
public sealed class TaskExecutorFactory(IServiceProvider serviceProvider, TaskExecutorRegistry registry, CallTaskExecutorRegistry callRegistry)
    : ITaskExecutorFactory
{

    /// <inheritdoc/>
    public ITaskExecutor Create(ITaskExecutionContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        if (context.Definition is CallTaskDefinition callDefinition)
        {
            var callExecutorType = callRegistry.Resolve(callDefinition.Call) ?? typeof(CustomFunctionCallTaskExecutor);
            return (ITaskExecutor)ActivatorUtilities.CreateInstance(serviceProvider, callExecutorType, context);
        }
        var executorType = registry.Resolve(context.Definition.Type) ?? throw new InvalidOperationException($"No task executor registered for task definition type '{context.Definition.GetType().Name}'");
        return (ITaskExecutor)ActivatorUtilities.CreateInstance(serviceProvider, executorType, context);
    }

    /// <inheritdoc/>
    public ITaskExecutor<TDefinition> Create<TDefinition>(ITaskExecutionContext<TDefinition> context)
        where TDefinition : TaskDefinition
    {
        ArgumentNullException.ThrowIfNull(context);
        if (context.Definition is CallTaskDefinition callDefinition)
        {
            var callExecutorType = callRegistry.Resolve(callDefinition.Call) ?? typeof(CustomFunctionCallTaskExecutor);
            return (ITaskExecutor<TDefinition>)ActivatorUtilities.CreateInstance(serviceProvider, callExecutorType, context);
        }
        var executorType = registry.Resolve(context.Definition.Type) ?? throw new InvalidOperationException($"No task executor registered for task type '{context.Definition.Type}'");
        return (ITaskExecutor<TDefinition>)ActivatorUtilities.CreateInstance(serviceProvider, executorType, context);
    }

}
