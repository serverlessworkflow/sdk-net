namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Defines the fundamentals of a service used to create, run and manage the lifecycle of <see cref="IContainer"/>s.
/// </summary>
public interface IContainerRuntime
{

    /// <summary>
    /// Creates a new container based on the specified <see cref="ContainerProcessDefinition"/>
    /// </summary>
    /// <param name="definition">The <see cref="ContainerProcessDefinition"/> that defines the container to create</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="IContainer"/></returns>
    Task<IContainer> CreateAsync(ContainerProcessDefinition definition, CancellationToken cancellationToken = default);

}
