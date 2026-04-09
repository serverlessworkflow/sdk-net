namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents a registry used to map process type discriminators (e.g. "container", "shell") to their corresponding <see cref="ITaskExecutor"/> types
/// </summary>
public sealed class RunTaskExecutorRegistry
{

    readonly Dictionary<string, Type> registry = [];

    /// <summary>
    /// Registers the specified <see cref="ITaskExecutor{TDefinition}"/> for the specified process type
    /// </summary>
    /// <param name="processType">The process type discriminator to register the executor for (e.g. "container", "shell", "script", "workflow")</param>
    /// <typeparam name="TExecutor">The type of <see cref="ITaskExecutor{TDefinition}"/> to register</typeparam>
    public void Register<TExecutor>(string processType)
        where TExecutor : class, ITaskExecutor<RunTaskDefinition>
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(processType);
        registry[processType] = typeof(TExecutor);
    }

    /// <summary>
    /// Resolves the <see cref="ITaskExecutor"/> type registered for the specified process type
    /// </summary>
    /// <param name="processType">The process type discriminator to resolve the executor for</param>
    /// <returns>The resolved executor type, if any</returns>
    public Type? Resolve(string processType)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(processType);
        return registry.TryGetValue(processType, out var executorType) ? executorType : null;
    }

}
