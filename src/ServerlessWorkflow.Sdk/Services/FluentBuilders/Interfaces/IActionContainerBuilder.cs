namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service that defines an <see cref="ActionDefinition"/>
/// </summary>
/// <typeparam name="TContainer">The container's type</typeparam>
public interface IActionContainerBuilder<TContainer>
      where TContainer : class, IActionContainerBuilder<TContainer>
{

    /// <summary>
    /// Creates and configures a new <see cref="ActionDefinition"/> to be executed by the container
    /// </summary>
    /// <param name="action">The <see cref="ActionDefinition"/> to execute</param>
    /// <returns>The configured container</returns>
    TContainer Execute(ActionDefinition action);

    /// <summary>
    /// Creates and configures a new <see cref="ActionDefinition"/> to be executed by the container
    /// </summary>
    /// <param name="actionSetup">An <see cref="Action{T}"/> used to setup the <see cref="ActionDefinition"/> to execute</param>
    /// <returns>The configured container</returns>
    TContainer Execute(Action<IActionBuilder> actionSetup);

    /// <summary>
    /// Creates and configures a new <see cref="ActionDefinition"/> to be executed by the container
    /// </summary>
    /// <param name="name">The name of the <see cref="ActionDefinition"/> to execute</param>
    /// <param name="actionSetup">An <see cref="Action{T}"/> used to setup the <see cref="ActionDefinition"/> to execute</param>
    /// <returns>The configured container</returns>
    TContainer Execute(string name, Action<IActionBuilder> actionSetup);

}
