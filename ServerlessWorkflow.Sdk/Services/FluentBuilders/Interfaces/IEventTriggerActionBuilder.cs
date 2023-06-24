namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="ActionDefinition"/>s of type <see cref="ActionType.Trigger"/>
/// </summary>
public interface IEventTriggerActionBuilder
{

    /// <summary>
    /// Configures the <see cref="ActionDefinition"/> to produce the specified <see cref="EventDefinition"/> when triggered
    /// </summary>
    /// <param name="e">The reference name of the <see cref="EventDefinition"/> to produce. Requires the referenced <see cref="EventDefinition"/> to have been previously defined.</param>
    /// <returns>The configured <see cref="IEventTriggerActionBuilder"/></returns>
    IEventTriggerActionBuilder ThenProduce(string e);

    /// <summary>
    /// Configures the <see cref="ActionDefinition"/> to produce the specified <see cref="EventDefinition"/> when triggered
    /// </summary>
    /// <param name="eventSetup">The <see cref="Action{T}"/> used to create the <see cref="EventDefinition"/> to produce</param>
    /// <returns>The configured <see cref="IActionBuilder"/></returns>
    IEventTriggerActionBuilder ThenProduce(Action<IEventBuilder> eventSetup);

    /// <summary>
    /// Adds the specified context attribute to the <see cref="CloudEvent"/> produced as a result of the trigger
    /// </summary>
    /// <param name="name">The name of the <see cref="CloudEvent"/> context attribute to add</param>
    /// <param name="value">The value of the <see cref="CloudEvent"/> context attribute to add</param>
    /// <returns>The configured <see cref="IEventTriggerActionBuilder"/></returns>
    IEventTriggerActionBuilder WithContextAttribute(string name, string value);

    /// <summary>
    /// Adds the specified context attribute to the <see cref="CloudEvent"/> produced as a result of the trigger
    /// </summary>
    /// <param name="contextAttributes">An <see cref="IDictionary{TKey, TValue}"/> containing the context attributes to add to the <see cref="CloudEvent"/>e produced as a result of the trigger</param>
    /// <returns>The configured <see cref="IEventTriggerActionBuilder"/></returns>
    IEventTriggerActionBuilder WithContextAttributes(IDictionary<string, string> contextAttributes);

    /// <summary>
    /// Builds the <see cref="ActionDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="ActionDefinition"/></returns>
    ActionDefinition Build();

}
