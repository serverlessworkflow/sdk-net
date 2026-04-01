namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the configuration of an event consumption strategy
/// </summary>
[Description("Represents the configuration of an event consumption strategy")]
[DataContract]
public sealed record EventConsumptionStrategyDefinition
{

    /// <summary>
    /// Gets/sets a list containing all the events that must be consumed, if any
    /// </summary>
    [Description("A list containing all the events that must be consumed, if any")]
    [DataMember(Order = 1, Name = "all"), JsonPropertyOrder(1), JsonPropertyName("all")]
    public EquatableList<EventFilterDefinition>? All { get; init; }

    /// <summary>
    /// Gets/sets a list containing any of the events to consume, if any.<para></para>
    /// If empty, listens to all incoming events, and requires <see cref="Until"/> to be set.
    /// </summary>
    [Description("A list containing any of the events to consume, if any. If empty, listens to all incoming events, and requires Until to be set.")]
    [DataMember(Order = 2, Name = "any"), JsonPropertyOrder(2), JsonPropertyName("any")]
    public EquatableList<EventFilterDefinition>? Any { get; init; }

    /// <summary>
    /// Gets/sets the single event to consume
    /// </summary>
    [Description("The single event to consume")]
    [DataMember(Order = 3, Name = "one"), JsonPropertyOrder(3), JsonPropertyName("one")]
    public EventFilterDefinition? One { get; init; }

    /// <summary>
    /// Gets/sets the condition or the consumption strategy that defines the events that must be consumed to stop listening
    /// </summary>
    [Description("The condition or the consumption strategy that defines the events that must be consumed to stop listening")]
    [DataMember(Order = 4, Name = "until"), JsonPropertyOrder(4), JsonPropertyName("until"), JsonConverter(typeof(OneOfJsonConverter<EventConsumptionStrategyDefinition, string>))]
    public OneOf<EventConsumptionStrategyDefinition, string>? Until { get; init; }

}
