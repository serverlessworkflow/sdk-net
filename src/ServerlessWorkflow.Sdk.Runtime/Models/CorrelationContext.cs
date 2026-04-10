namespace ServerlessWorkflow.Sdk.Runtime.Models;

/// <summary>
/// Represents an object used to describe the context of a correlation
/// </summary>
[DataContract]
public sealed record CorrelationContext
{

    /// <summary>
    /// Gets/sets the context's unique identifier
    /// </summary>
    [DataMember(Name = "id", Order = 1), JsonPropertyName("id"), JsonPropertyOrder(1)]
    public required string Id { get; init; }

    /// <summary>
    /// Gets/sets the context's status
    /// </summary>
    [DataMember(Name = "status", Order = 2), JsonPropertyName("status"), JsonPropertyOrder(2)]
    public string Status { get; init; } = CorrelationContextStatus.Active;

    /// <summary>
    /// Gets a key/value mapping of the context's correlation keys
    /// </summary>
    [DataMember(Name = "keys", Order = 3), JsonPropertyName("keys"), JsonPropertyOrder(3)]
    public EquatableDictionary<string, string> Keys { get; init; } = [];

    /// <summary>
    /// Gets a key/value mapping of all correlated events, with the key being the index of the matched correlation filter
    /// </summary>
    [DataMember(Name = "events", Order = 4), JsonPropertyName("events"), JsonPropertyOrder(4)]
    public EquatableDictionary<int, CloudEvent> Events { get; init; } = [];

    /// <summary>
    /// Gets the offset that serves as the index of the event being processed by the consumer, if streaming has been enabled for the correlation associated with the context.
    /// </summary>
    [DataMember(Name = "offset", Order = 5), JsonPropertyName("offset"), JsonPropertyOrder(5)]
    public uint? Offset { get; init; }

}