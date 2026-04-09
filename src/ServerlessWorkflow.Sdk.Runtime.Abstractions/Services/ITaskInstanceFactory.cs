namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Defines the fundamentals of a service used to create <see cref="ITaskInstance"/>s
/// </summary>
public interface ITaskInstanceFactory
{

    /// <summary>
    /// Creates a new <see cref="ITaskInstance"/> based on the specified <see cref="TaskDefinition"/> and input
    /// </summary>
    /// <param name="definition">The <see cref="TaskDefinition"/> to create the <see cref="ITaskInstance"/> from</param>
    /// <param name="input">The input to initialize the <see cref="ITaskInstance"/> with</param>
    /// <returns>A new <see cref="ITaskInstance"/></returns>
    ITaskInstance Create(TaskDefinition definition, JsonObject input);

}