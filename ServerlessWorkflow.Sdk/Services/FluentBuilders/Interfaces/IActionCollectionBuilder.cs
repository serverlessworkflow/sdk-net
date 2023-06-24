namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build a collection of <see cref="ActionDefinition"/>s
/// </summary>
/// <typeparam name="TContainer"></typeparam>
public interface IActionCollectionBuilder<TContainer>
    : IActionContainerBuilder<TContainer>
    where TContainer : class, IActionCollectionBuilder<TContainer>
{

    /// <summary>
    /// Configures the container to run defined actions sequentially
    /// </summary>
    /// <returns>The configured container</returns>
    TContainer Sequentially();

    /// <summary>
    /// Configures the container to run defined actions concurrently
    /// </summary>
    /// <returns>The configured container</returns>
    TContainer Concurrently();

}
