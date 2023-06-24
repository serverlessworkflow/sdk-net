namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="EventDefinition"/>s
/// </summary>
public interface IEventBuilder
    : IMetadataContainerBuilder<IEventBuilder>
{

    /// <summary>
    /// Sets the name of the <see cref="EventDefinition"/> to build
    /// </summary>
    /// <param name="name">The name of the <see cref="EventDefinition"/> to build</param>
    /// <returns>The configured <see cref="IEventBuilder"/></returns>
    IEventBuilder WithName(string name);

    /// <summary>
    /// Sets the <see cref="EventDefinition"/>'s <see cref="EventDefinition.Kind"/> to <see cref="EventKind.Consumed"/>
    /// </summary>
    /// <returns>The configured <see cref="IEventBuilder"/></returns>
    IEventBuilder IsConsumed();

    /// <summary>
    /// Sets the <see cref="EventDefinition"/>'s <see cref="EventDefinition.Kind"/> to <see cref="EventKind.Produced"/>
    /// </summary>
    /// <returns>The configured <see cref="IEventBuilder"/></returns>
    IEventBuilder IsProduced();

    /// <summary>
    /// Sets the <see cref="CloudEvent"/> source of the <see cref="EventDefinition"/> to build
    /// </summary>
    /// <param name="source">The <see cref="CloudEvent"/> source of the <see cref="EventDefinition"/> to build</param>
    /// <returns>The configured <see cref="IEventBuilder"/></returns>
    IEventBuilder WithSource(Uri source);

    /// <summary>
    /// Sets the <see cref="CloudEvent"/> type of the <see cref="EventDefinition"/> to build
    /// </summary>
    /// <param name="type">The <see cref="CloudEvent"/> type of the <see cref="EventDefinition"/> to build</param>
    /// <returns>The configured <see cref="IEventBuilder"/></returns>
    IEventBuilder WithType(string type);

    /// <summary>
    /// Configures the <see cref="EventDefinition"/> to use the specified <see cref="CloudEvent"/> context attribute while performing correlation
    /// </summary>
    /// <param name="contextAttributeName">The name of the <see cref="CloudEvent"/> context attribute to use</param>
    /// <returns>The configured <see cref="IEventBuilder"/></returns>
    IEventBuilder CorrelateUsing(string contextAttributeName);

    /// <summary>
    /// Configures the <see cref="EventDefinition"/> to use the specified <see cref="CloudEvent"/> context attribute while performing correlation
    /// </summary>
    /// <param name="contextAttributeName">The name of the <see cref="CloudEvent"/> context attribute to use</param>
    /// <param name="contextAttributeValue">The static value or workflow expression used during correlation</param>
    /// <returns>The configured <see cref="IEventBuilder"/></returns>
    IEventBuilder CorrelateUsing(string contextAttributeName, string contextAttributeValue);

    /// <summary>
    /// Configures the <see cref="EventDefinition"/> to use the specified <see cref="CloudEvent"/> context attribute while performing correlation
    /// </summary>
    /// <param name="correlations">A <see cref="IDictionary{TKey, TValue}"/> containing the context attribute key/value pairs to used when performing correlation</param>
    /// <returns>The configured <see cref="IEventBuilder"/></returns>
    IEventBuilder CorrelateUsing(IDictionary<string, string> correlations);

    /// <summary>
    /// Builds the <see cref="EventDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="EventDefinition"/></returns>
    EventDefinition Build();

}
