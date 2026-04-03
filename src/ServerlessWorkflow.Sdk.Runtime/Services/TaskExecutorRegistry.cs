namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents a registry used to map task type discriminators to their corresponding <see cref="ITaskExecutor"/> types
/// </summary>
public sealed class TaskExecutorRegistry
{

    static readonly Dictionary<Type, string> taskDefinitionTypeMap = new()
    {
        [typeof(CallTaskDefinition)] = TaskType.Call,
        [typeof(DoTaskDefinition)] = TaskType.Do,
        [typeof(EmitTaskDefinition)] = TaskType.Emit,
        [typeof(ExtensionTaskDefinition)] = TaskType.Extension,
        [typeof(ForTaskDefinition)] = TaskType.For,
        [typeof(ForkTaskDefinition)] = TaskType.Fork,
        [typeof(ListenTaskDefinition)] = TaskType.Listen,
        [typeof(RaiseTaskDefinition)] = TaskType.Raise,
        [typeof(RunTaskDefinition)] = TaskType.Run,
        [typeof(SetTaskDefinition)] = TaskType.Set,
        [typeof(SwitchTaskDefinition)] = TaskType.Switch,
        [typeof(TryTaskDefinition)] = TaskType.Try,
        [typeof(WaitTaskDefinition)] = TaskType.Wait,
    };
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
    /// Registers the specified <see cref="ITaskExecutor{TDefinition}"/> for the specified <see cref="TaskDefinition"/> type
    /// </summary>
    /// <typeparam name="TDefinition">The type of <see cref="TaskDefinition"/> to register the executor for</typeparam>
    /// <typeparam name="TExecutor">The type of <see cref="ITaskExecutor{TDefinition}"/> to register</typeparam>
    public void Register<TDefinition, TExecutor>()
        where TDefinition : TaskDefinition
        where TExecutor : class, ITaskExecutor<TDefinition>
    {
        if (!taskDefinitionTypeMap.TryGetValue(typeof(TDefinition), out var taskType)) throw new InvalidOperationException($"Unknown task definition type '{typeof(TDefinition).Name}'. Use Register<TExecutor>(string taskType) for custom task types.");
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
