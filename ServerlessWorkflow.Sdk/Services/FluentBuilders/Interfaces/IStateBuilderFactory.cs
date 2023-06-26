namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to create <see cref="IStateBuilder{TState}"/> instances
/// </summary>
public interface IStateBuilderFactory
{

    /// <summary>
    /// Creates and configures a new <see cref="ICallbackStateBuilder"/>
    /// </summary>
    /// <returns>A new <see cref="ICallbackStateBuilder"/></returns>
    ICallbackStateBuilder Callback();

    /// <summary>
    /// Creates and configures a new <see cref="SleepStateDefinition"/>
    /// </summary>
    /// <param name="duration">The delay's duration</param>
    /// <returns>A new <see cref="IDelayStateBuilder"/></returns>
    IDelayStateBuilder Delay(TimeSpan duration);

    /// <summary>
    /// Creates and configures a new <see cref="SleepStateDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="IDelayStateBuilder"/></returns>
    IDelayStateBuilder Delay();

    /// <summary>
    /// Creates and configures a new <see cref="InjectStateDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="IInjectStateBuilder"/></returns>
    IInjectStateBuilder Inject();

    /// <summary>
    /// Creates and configures a new <see cref="InjectStateDefinition"/>
    /// </summary>
    /// <param name="data">The data to inject</param>
    /// <returns>A new <see cref="IInjectStateBuilder"/></returns>
    IInjectStateBuilder Inject(object data);

    /// <summary>
    /// Creates and configures a new <see cref="ExtensionStateDefinition"/>
    /// </summary>
    /// <param name="type">The type of the <see cref="ExtensionStateDefinition"/> to build</param>
    /// <returns>A new <see cref="IExtensionStateBuilder"/></returns>
    IExtensionStateBuilder Extension(string type);

    /// <summary>
    /// Creates and configures a new <see cref="OperationStateDefinition"/>
    /// </summary>
    /// <param name="action">The <see cref="ActionDefinition"/> to execute</param>
    /// <returns>A new <see cref="IOperationStateBuilder"/></returns>
    IOperationStateBuilder Execute(ActionDefinition action);

    /// <summary>
    /// Creates and configures a new <see cref="OperationStateDefinition"/>
    /// </summary>
    /// <param name="actionSetup">An <see cref="Action{T}"/> used to setup the <see cref="ActionDefinition"/> to execute</param>
    /// <returns>A new <see cref="IOperationStateBuilder"/></returns>
    IOperationStateBuilder Execute(Action<IActionBuilder> actionSetup);

    /// <summary>
    /// Creates and configures a new <see cref="OperationStateDefinition"/>
    /// </summary>
    /// <param name="name">the name of the <see cref="ActionDefinition"/> to execute</param>
    /// <param name="actionSetup">An <see cref="Action{T}"/> used to setup the <see cref="ActionDefinition"/> to execute</param>
    /// <returns>A new <see cref="IOperationStateBuilder"/></returns>
    IOperationStateBuilder Execute(string name, Action<IActionBuilder> actionSetup);

    /// <summary>
    /// Creates and configures a new <see cref="ParallelStateDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="IParallelStateBuilder"/></returns>
    IParallelStateBuilder ExecuteInParallel();

    /// <summary>
    /// Creates and configures a new <see cref="ForEachStateDefinition"/>
    /// </summary>
    /// <param name="inputCollection">An expression that defines the input collection</param>
    /// <param name="iterationParameter">An expression that defines the iteration parameter</param>
    /// <param name="outputCollection">An expression that defines the output collection</param>
    /// <returns>A new <see cref="IForEachStateBuilder"/></returns>
    IForEachStateBuilder ForEach(string inputCollection, string iterationParameter, string outputCollection);

    /// <summary>
    /// Creates and configures a new data-based <see cref="SwitchStateDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="IDataSwitchStateBuilder"/></returns>
    IDataSwitchStateBuilder Switch();

    /// <summary>
    /// Creates and configures a new <see cref="CloudEvent"/>-based <see cref="SwitchStateDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="IEventSwitchStateBuilder"/></returns>
    IEventSwitchStateBuilder SwitchEvents();

    /// <summary>
    /// Creates and configures a new <see cref="IEventStateBuilder"/>
    /// </summary>
    /// <returns>A new <see cref="IEventStateBuilder"/></returns>
    IEventStateBuilder Events();

}
