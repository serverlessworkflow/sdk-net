namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Defines the fundamentals of a service used to build collections of <see cref="EventFilterDefinition"/>s
/// </summary>
public interface IEventFilterDefinitionCollectionBuilder
{

    /// <summary>
    /// Adds the specified event filter to the collection
    /// </summary>
    /// <param name="filter">The filter to add</param>
    /// <returns>The configured <see cref="IEventFilterDefinitionCollectionBuilder"/></returns>
    IEventFilterDefinitionCollectionBuilder Event(EventFilterDefinition filter);

    /// <summary>
    /// Adds the specified event filter to the collection
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the filter to add</param>
    /// <returns>The configured <see cref="IEventFilterDefinitionCollectionBuilder"/></returns>
    IEventFilterDefinitionCollectionBuilder Event(Action<IEventFilterDefinitionBuilder> setup);

    /// <summary>
    /// Builds the configured collection of <see cref="EventFilterDefinition"/>s
    /// </summary>
    /// <returns>A new collection of <see cref="EventFilterDefinition"/>s</returns>
    EquatableList<EventFilterDefinition> Build();

}
