namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents a registry used to map task type discriminators to their corresponding <see cref="ITaskExecutor"/> types
/// </summary>
public sealed class TaskExecutorRegistry
{

    readonly Dictionary<string, Type> registry = [];

    /// <summary>
    /// Registers the specified <see cref="ITaskExecutor"/> type for the specified task type
    /// </summary>
    /// <param name="taskType">The task type discriminator to register the executor for</param>
    /// <typeparam name="TExecutor">The type of <see cref="ITaskExecutor"/> to register</typeparam>
    public void Register<TExecutor>(string taskType)
        where TExecutor : class, ITaskExecutor
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(taskType);
        registry[taskType] = typeof(TExecutor);
    }

    /// <summary>
    /// Resolves the <see cref="ITaskExecutor"/> type registered for the specified task type
    /// </summary>
    /// <param name="taskType">The task type discriminator to resolve the executor for</param>
    /// <returns>The resolved executor type, if any</returns>
    public Type? Resolve(string taskType)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(taskType);
        return registry.TryGetValue(taskType, out var executorType) ? executorType : null;
    }

}
