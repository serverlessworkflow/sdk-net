namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Defines the fundamentals of a service used to create <see cref="ITaskExecutor"/>s
/// </summary>
public interface ITaskExecutorFactory
{

    /// <summary>
    /// Creates a new <see cref="ITaskExecutor"/> for the specified <see cref="ITaskInstance"/>
    /// </summary>
    /// <param name="process">The <see cref="ITaskProcess"/> to create a new <see cref="ITaskExecutor"/> for</param>
    /// <returns>A new <see cref="ITaskExecutor"/> for the specified <see cref="ITaskInstance"/></returns>
    ITaskExecutor Create(ITaskProcess process);

    /// <summary>
    /// Creates a new <see cref="ITaskExecutor"/> for the specified <see cref="ITaskInstance"/>
    /// </summary>
    /// <typeparam name="TDefinition">The <see cref="TaskDefinition"/> of the <see cref="ITaskInstance"/> to execute</typeparam>
    /// <param name="process">The <see cref="ITaskProcess"/> to create a new <see cref="ITaskExecutor"/> for</param>
    /// <returns>A new <see cref="ITaskExecutor"/> for the specified <see cref="ITaskInstance"/></returns>
    ITaskExecutor<TDefinition> Create<TDefinition>(ITaskProcess<TDefinition> process)
        where TDefinition : TaskDefinition;

}