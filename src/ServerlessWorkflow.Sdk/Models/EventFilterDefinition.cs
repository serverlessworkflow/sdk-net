namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the configuration of an event filter
/// </summary>
[Description("Represents the configuration of an event filter")]
[DataContract]
public sealed record EventFilterDefinition
{

    /// <summary>
    /// Gets/sets a name/value mapping of the attributes filtered events must define. Supports both regular expressions and runtime expressions.
    /// </summary>
    [Description("A name/value mapping of the attributes filtered events must define. Supports both regular expressions and runtime expressions.")]
    [DataMember(Order = 1, Name = "with"), JsonPropertyOrder(1), JsonPropertyName("with")]
    public JsonObject? With { get; init; }

    /// <summary>
    /// Gets/sets a name/definition mapping of the correlation to attempt when filtering events.
    /// </summary>
    [Description("A name/definition mapping of the correlation to attempt when filtering events.")]
    [DataMember(Order = 2, Name = "correlate"), JsonPropertyOrder(2), JsonPropertyName("correlate")]
    public EquatableDictionary<string, CorrelationKeyDefinition>? Correlate { get; init; }

}
