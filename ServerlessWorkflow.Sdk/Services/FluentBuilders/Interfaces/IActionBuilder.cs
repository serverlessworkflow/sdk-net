namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="ActionDefinition"/>s
/// </summary>
public interface IActionBuilder
{

    /// <summary>
    /// Sets the name of the <see cref="ActionDefinition"/> to build
    /// </summary>
    /// <param name="name">The name of the <see cref="ActionDefinition"/> to build</param>
    /// <returns>The configured <see cref="IActionBuilder"/></returns>
    IActionBuilder WithName(string name);

    /// <summary>
    /// Configures the workflow expression used to filter the state data passed to the <see cref="ActionDefinition"/>
    /// </summary>
    /// <param name="expression">The workflow expression used to filter the <see cref="ActionDefinition"/>'s input state data</param>
    /// <returns>The configured <see cref="IActionBuilder"/></returns>
    IActionBuilder FromStateData(string expression);

    /// <summary>
    /// Configures the workflow expression used to filter the <see cref="ActionDefinition"/>'s results
    /// </summary>
    /// <param name="expression">The workflow expression used to filter the <see cref="ActionDefinition"/>'s results</param>
    /// <returns>The configured <see cref="IActionBuilder"/></returns>
    IActionBuilder FilterResults(string expression);

    /// <summary>
    /// Configures the workflow expression used to merge the <see cref="ActionDefinition"/>'s results into the state data
    /// </summary>
    /// <param name="expression">The workflow expression used to merge the <see cref="ActionDefinition"/>'s results into the state data</param>
    /// <returns>The configured <see cref="IActionBuilder"/></returns>
    IActionBuilder ToStateData(string expression);

    /// <summary>
    /// Invokes the specified <see cref="FunctionDefinition"/>
    /// </summary>
    /// <param name="function">The reference name of the <see cref="FunctionDefinition"/> to invoke. Requires the referenced <see cref="FunctionDefinition"/> to have been previously defined</param>
    /// <returns>The configured <see cref="IActionBuilder"/></returns>
    IFunctionActionBuilder Invoke(string function);

    /// <summary>
    /// Invokes the specified <see cref="FunctionDefinition"/>
    /// </summary>
    /// <param name="functionSetup">An <see cref="Action{T}"/> used to setup the <see cref="FunctionDefinition"/> to invoke</param>
    /// <returns>The configured <see cref="IActionBuilder"/></returns>
    IFunctionActionBuilder Invoke(Action<IFunctionBuilder> functionSetup);

    /// <summary>
    /// Invokes the specified <see cref="FunctionDefinition"/>
    /// </summary>
    /// <param name="function">The <see cref="FunctionDefinition"/> to invoke</param>
    /// <returns>The configured <see cref="IActionBuilder"/></returns>
    IFunctionActionBuilder Invoke(FunctionDefinition function);

    /// <summary>
    /// Configures the <see cref="ActionDefinition"/> to build to consume the specified <see cref="EventDefinition"/>
    /// </summary>
    /// <param name="e">The reference name of the <see cref="EventDefinition"/> to consume. Requires the referenced <see cref="EventDefinition"/> to have been previously defined</param>
    /// <returns>The configured <see cref="IActionBuilder"/></returns>
    IEventTriggerActionBuilder Consume(string e);

    /// <summary>
    /// Configures the <see cref="ActionDefinition"/> to build to consume the specified <see cref="EventDefinition"/>
    /// </summary>
    /// <param name="eventSetup">The <see cref="Action{T}"/> used to create the <see cref="EventDefinition"/> to consume</param>
    /// <returns>The configured <see cref="IActionBuilder"/></returns>
    IEventTriggerActionBuilder Consume(Action<IEventBuilder> eventSetup);

    /// <summary>
    /// Configures the <see cref="ActionDefinition"/> to build to consume the specified <see cref="EventDefinition"/>
    /// </summary>
    /// <param name="e">The <see cref="EventDefinition"/> to consume</param>
    /// <returns>The configured <see cref="IActionBuilder"/></returns>
    IEventTriggerActionBuilder Consume(EventDefinition e);

    /// <summary>
    /// Configures the <see cref="ActionDefinition"/> to build to run the specified workflow definition
    /// </summary>
    /// <param name="workflowId">The id of the workflow definition to run</param>
    /// <param name="version">The version of the workflow definition to run</param>
    /// <param name="invocationMode">The <see cref="ActionDefinition"/>'s <see cref="InvocationMode"/>. Defaults to <see cref="InvocationMode.Synchronous"/></param>
    /// <returns>The service used to build <see cref="SubflowReference"/>s</returns>
    ISubflowActionBuilder Run(string workflowId, string version, string invocationMode = InvocationMode.Synchronous);

    /// <summary>
    /// Configures the <see cref="ActionDefinition"/> to build to run the specified workflow definition
    /// </summary>
    /// <param name="workflowId">The id of the workflow definition to run</param>
    /// <param name="invocationMode">The <see cref="ActionDefinition"/>'s <see cref="InvocationMode"/>. Defaults to <see cref="InvocationMode.Synchronous"/></param>
    /// <returns>The service used to build <see cref="SubflowReference"/>s</returns>
    ISubflowActionBuilder Run(string workflowId, string invocationMode = InvocationMode.Synchronous);

    /// <summary>
    /// Builds the <see cref="ActionDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="ActionDefinition"/></returns>
    ActionDefinition Build();

}
