namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of an event correlation key
/// </summary>
[Description("Represents the definition of an event correlation key")]
[DataContract]
public sealed record CorrelationKeyDefinition
{

    /// <summary>
    /// Gets/sets a runtime expression used to extract the correlation key value from events.
    /// </summary>
    [Description("A runtime expression used to extract the correlation key value from events.")]
    [DataMember(Order = 1, Name = "from"), JsonPropertyOrder(1), JsonPropertyName("from")]
    public required string From { get; init; }

    /// <summary>
    /// Gets/sets a constant or a runtime expression, if any, used to determine whether or not the extracted correlation key value matches expectations and should be correlated. If not set, the first extracted value will be used as the correlation key's expectation.
    /// </summary>
    [Description("A constant or a runtime expression, if any, used to determine whether or not the extracted correlation key value matches expectations and should be correlated. If not set, the first extracted value will be used as the correlation key's expectation.")]
    [DataMember(Order = 2, Name = "expect"), JsonPropertyOrder(2), JsonPropertyName("expect")]
    public string? Expect { get; init; }

}
